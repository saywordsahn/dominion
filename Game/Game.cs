using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common;
using DominionWeb.Game.GameComponents;
using DominionWeb.Game.Player;
using DominionWeb.Game.Supply;
using Newtonsoft.Json;

namespace DominionWeb.Game
{
    public class Game
    {
        public readonly int GameId;
        public IList<IPlayer> Players { get; private set; }
        public ISupply Supply { get; private set; }
        public GameStatus GameStatus { get; private set; }
        public Components Components { get; set; }

        public IVictoryCondition VictoryCondition { get; private set; }

        public Game(int gameId, IList<IPlayer> players, ISupply supply, IVictoryCondition victoryCondition)
        {
            GameId = gameId;
            Players = players;
            Supply = supply;
            VictoryCondition = victoryCondition;
            Components = new Components();
        }

        public void Initialize()
        {
            GameStatus = GameStatus.Active;
            bool first = true;
            
            foreach (var player in Players)
            {
                var coppers = Enumerable.Repeat(Supply.Take(Card.Copper), 7);
                var estates = Enumerable.Repeat(Supply.Take(Card.Estate), 3);
                var witches = Enumerable.Repeat(Card.Witch, 3);
                var thrones = Enumerable.Repeat(Card.ThroneRoom, 3);
                player.Gain(coppers);
//                Console.WriteLine("{0} starts with 7 Coppers.", player.Name);
//                Console.WriteLine("{0} starts with 3 Estates.", player.Name);
                player.Gain(estates);
//                player.Gain(witches);
//                player.Gain(thrones);
                player.Shuffle();
//                Console.WriteLine("{0} shuffles their deck.", player.Name);
                player.Draw(5);

                if (first)
                {
                    player.PlayStatus = PlayStatus.BuyPhase;
                    player.NumberOfBuys = 1;
                    first = false;
                }
                else
                {
                    player.PlayStatus = PlayStatus.WaitForTurn;
                }
            }
        }

        //TODO: heavy need of refactoring
        public void CheckPlayStack(IPlayer player)
        {

            bool CanPlayRule(IPlayer p) => p.Rules.All(x => x.Resolved) && p.RuleStack.Count > 0;
//                                           && p.PlayStatus == PlayStatus.AttackResponder;

            bool CanPlayCard(IPlayer p) =>
                p.PlayStatus == PlayStatus.ActionPhase && p.PlayStack.Count > 0;
            
            while ( CanPlayRule(player) || CanPlayCard(player))
            {
                //rules are only implemented for AttackResponder for the moment - this will change but to
                //narrow the scope of this feature we use it soley for now
                while (CanPlayRule(player))
                {
                    var rule = player.RuleStack.Pop();

                    player.Rules.Add(rule);

                    rule.Resolve(this, player);
                }

                //we need to handle player switches in rules
                //should be less ugly once we refactor
                if (player != GetActivePlayer())
                {
                    player = GetActivePlayer();

                    if (player == null) return;
                    continue;
                }

                while (CanPlayCard(player))
                {
                    var card = player.PlayStack.Pop();

                    if (card.IsThronedCopy)
                    {
                        player.PlayedCards.Add(card);
                    }
                    
                    if (card.Card is IRulesHolder rh)
                    {
                        foreach (var rule in rh.GetRules(this, player))
                        {
                            player.RuleStack.Push(rule);
                        }
                    }
                    else if (card.Card is IAction a)
                    {
                        a.Resolve(this);
                    }
                }
                
                if (player != GetActivePlayer())
                {
                    player = GetActivePlayer();
                    if (player == null) return;
                    continue;
                }
                
//                break;
            }
        }

        public void Submit(string playerName, PlayerAction action, Card card)
        {
            var player = Players.Single(x => x.PlayerName == playerName);
            var instance = CardFactory.Create(card);
            
            //TODO: consider combining ActionAttack into one status/interface
            if (action == PlayerAction.Play && instance is IAction a1 && instance is IAttack att)
            {
                player.Play(instance);
                a1.Resolve(this);
                //with attack cards the player switches so we need to get the updated player
                var activePlayer = GetActivePlayer();
                CheckPlayStack(activePlayer);
            }
            else if (action == PlayerAction.Play && player.PlayStatus == PlayStatus.ActionPhase &&
                     instance is IRulesHolder rh && player.NumberOfActions > 0)
            {
                player.Play(instance);
                foreach (var rule in rh.GetRules(this, player))
                {
                    player.RuleStack.Push(rule);
                }
//                player.RuleStack.Push(rh.GetRule(this, player));
                CheckPlayStack(player);
                //TODO: look into refactoring this to player object (ex. player.SetActionOrBuyPhase())
                //TODO: check for number of actions remaining to move statuses automatically
                if (player.PlayStatus != PlayStatus.ActionRequestResponder)
                {
                    player.PlayStatus = player.HasActionInHand() ? PlayStatus.ActionPhase : PlayStatus.BuyPhase;
                }
            }
            else if (action == PlayerAction.Play && player.PlayStatus == PlayStatus.ActionPhase && instance is IAction a2
                     && player.NumberOfActions > 0)
            {
                player.Play(instance);
                a2.Resolve(this);
                CheckPlayStack(player);
                //TODO: look into refactoring this to player object (ex. player.SetActionOrBuyPhase())
                //TODO: check for number of actions remaining to move statuses automatically
                if (player.PlayStatus != PlayStatus.ActionRequestResponder)
                {
                    player.PlayStatus = player.HasActionInHand() ? PlayStatus.ActionPhase : PlayStatus.BuyPhase;
                }
                
                
            }
            else if (action == PlayerAction.Play && player.PlayStatus == PlayStatus.BuyPhase && instance is ITreasure t)
            {
                //we could implement by as an IAction and then we could remove this section
                //maybe a different abstraction is better though
                //TODO: refactor
                player.Play(instance);
                CheckPlayStack(player);
            }
            else if (action == PlayerAction.Buy)
            {
               
                if (Supply.CardIsVisible(card) && instance.Cost <= player.MoneyPlayed && player.NumberOfBuys >= 1
                    && (instance is IBuyConditionHolder bch && bch.ResolveBuyCondition(this, player) || !(instance is IBuyConditionHolder)))
                {
                    player.Buy(card);
                    Supply.Take(card);
                    CheckPlayStack(player);
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void Submit(string playerName, PlayerAction playerAction)
        {
            var player = Players.Single(x => x.PlayerName == playerName);
            var nextPlayer = GetNextPlayer(player);
            
            if (playerAction == PlayerAction.EndTurn)
            {
                player.EndTurn();
                CheckPlayStack(player);
                
                if (VictoryCondition.IsMet(Supply))
                {
                    OnGameEnd();
                }
                else
                {
                    nextPlayer.StartTurn(this);
                    CheckPlayStack(nextPlayer);
                }
            }
            else if (playerAction == PlayerAction.PlayAllTreasure && player.PlayStatus == PlayStatus.BuyPhase)
            {
                player.PlayAllTreasure();
            }
            else if (playerAction == PlayerAction.EndActionPhase)
            {
                player.EndActionPhase();
            }
            else if (playerAction == PlayerAction.PlayCoffer && player.PlayStatus == PlayStatus.BuyPhase && !player.HasBoughtThisTurn)
            {
                player.PlayCoffers(1);
            }
            else if (playerAction == PlayerAction.PlayAllCoffers && player.PlayStatus == PlayStatus.BuyPhase && !player.HasBoughtThisTurn)
            {
                player.PlayCoffers(player.Coffers);
            }
            else if (playerAction == PlayerAction.PlayVillager && player.PlayStatus == PlayStatus.ActionPhase)
            {
                player.PlayVillagers(1);
            }
           
        }
        
        private void OnGameEnd()
        {
            GameStatus = GameStatus.Finished;
            foreach (var player in Players)
            {
                player.PlayStatus = PlayStatus.GameEnd;
            }
        }

        public void Submit(string playerName, ActionRequestType actionRequestType, ActionResponse actionResponse)
        {
            var player = Players.Single(x => x.PlayerName == playerName);
            //var card = player.ActionRequest.Requester;

            //TODO: rethink this through, likely bugs, if not now, in future when more cards are added
            //var instance = player.PlayedCards[player.PlayedCards.Count - 1];
            if (player.Rules.Last().Resolved == false && player.PlayStatus == PlayStatus.ActionRequestResponder)
            {
                var rInstance = player.Rules.Last();

                if (rInstance is IResponseRequired<ActionResponse> rr)
                {
                    rr.ResponseReceived(this, actionResponse);
                }
                
                CheckPlayStack(player);
            }
            else
            {
                var instance = player.PlayedCards.Last(x => x.Card.Name == player.ActionRequest.Requester).Card;

                if (actionRequestType == ActionRequestType.YesNo && instance is IResponseRequired<ActionResponse> ar)
                {
                    ar.ResponseReceived(this, actionResponse);
                    CheckPlayStack(player);
                }
            }
            
        }
        
        public void Submit(string playerName, ActionRequestType actionRequestType, IEnumerable<Card> cards)
        {
            //for attack responders this won't work since they didn't play the card
            var player = Players.Single(x => x.PlayerName == playerName);
//            var card = player.ActionRequest.Requester;

            if (player.Rules.Count > 0 && player.Rules.Last().Resolved == false)
            {
                var rInstance = player.Rules.Last();

                if (rInstance is IResponseRequired<IEnumerable<Card>> rr)
                {
                    rr.ResponseReceived(this, cards);
                }
                
                CheckPlayStack(player);
            }
            else
            {
                var instance = player.PlayedCards.Last(x => x.Card.Name == player.ActionRequest.Requester).Card;

                if (actionRequestType == ActionRequestType.SelectCards && instance is IResponseRequired<IEnumerable<Card>> rr)
                {
                    rr.ResponseReceived(this, cards);
                    CheckPlayStack(player);
                }
            }
            
        }
        
        public void Submit(string playerName, ActionRequestType actionRequestType, IEnumerable<ActionResponse> options)
        {
            //for attack responders this won't work since they didn't play the card
            var player = Players.Single(x => x.PlayerName == playerName);
//            var card = player.ActionRequest.Requester;

            if (player.Rules.Count > 0 && player.Rules.Last().Resolved == false)
            {
                var rInstance = player.Rules.Last();

                if (rInstance is IResponseRequired<IEnumerable<ActionResponse>> rr)
                {
                    rr.ResponseReceived(this, options);
                }
                
                CheckPlayStack(player);
            }
//            else
//            {
//                var instance = player.PlayedCards.Last(x => x.Card.Name == player.ActionRequest.Requester).Card;
//
//                if (actionRequestType == ActionRequestType.SelectCards && instance is IResponseRequired<IEnumerable<Card>> rr)
//                {
//                    rr.ResponseReceived(this, cards);
//                    CheckPlayStack(player);
//                }
//            }
            
        }

        public IPlayer GetNextPlayer(IPlayer currentPlayer)
        {
            var index = Players.IndexOf(currentPlayer);           

            return index >= Players.Count - 1 ? Players[0] : Players[index + 1];
        }

        public IPlayer GetAttackingPlayer()
        {
            return Players.Single(x => x.PlayStatus == PlayStatus.Attacker);
        }
        
        public string GetGameState()
        {
//            var model = new GameStateModel();
//            model.GameId = GameId;
//            model.Players = Players;
//            model.Supply = Supply;
            
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }

        public IPlayer GetActivePlayer()
        {
            return Players.SingleOrDefault(x =>
                x.PlayStatus == PlayStatus.ActionPhase || x.PlayStatus == PlayStatus.BuyPhase
                || x.PlayStatus == PlayStatus.ActionRequestResponder
                || x.PlayStatus == PlayStatus.AttackResponder);
        }

        public static Game Load(string gameState)
        {
            var game = JsonConvert.DeserializeObject<Game>(gameState, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            return game;
        }

        private class GameStateModel
        {
            public int GameId { get; set; }
            public IList<IPlayer> Players { get; set; }
            public ISupply Supply { get; set; }
        }
    }
}
