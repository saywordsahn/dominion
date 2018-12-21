using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Common;
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

        public IVictoryCondition VictoryCondition { get; private set; }

        public Game(int gameId, IList<IPlayer> players, ISupply supply, IVictoryCondition victoryCondition)
        {
            GameId = gameId;
            Players = players;
            Supply = supply;
            VictoryCondition = victoryCondition;
        }

        public void Initialize()
        {
            GameStatus = GameStatus.Active;
            bool first = true;
            
            foreach (var player in Players)
            {
                var coppers = Enumerable.Repeat(Supply.Take(Card.Copper), 7);
                var estates = Enumerable.Repeat(Supply.Take(Card.Estate), 3);
                player.Gain(coppers);
//                Console.WriteLine("{0} starts with 7 Coppers.", player.Name);
//                Console.WriteLine("{0} starts with 3 Estates.", player.Name);
                player.Gain(estates);
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

        public void CheckPlayStack(IPlayer player)
        {
//            while (player.OnGainAbilities.Count > 0 
//                && (player.PlayedAbilities.Count == 0 || player.PlayedAbilities.Last().Resolved == true))
//            {
//                var ability = player.OnGainAbilities.Last();
//                player.OnGainAbilities.RemoveAt(player.OnGainAbilities.Count - 1);
//                player.PlayedAbilities.Add(ability);
//                ability.Resolve(player);
//            }

            while (!player.IsRespondingToAbility() && player.PlayedAbilities.Any(x => x.Resolved != true))
            {
                var ability = player.PlayedAbilities.First(x => x.Resolved == false);
                ability.Resolve(player);
            }
            
            while (player.PlayStatus == PlayStatus.ActionPhase && player.PlayStack.Count > 0
                && (player.PlayedAbilities.Count == 0 || player.PlayedAbilities.Last().Resolved == true) )
            {
                var card = player.PlayStack.Pop();

                if (card.IsThronedCopy)
                {
                    player.PlayedCards.Add(card);
                }
                
                if (card.Card is IAction a) a.Resolve(this);
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
            }
            else if (action == PlayerAction.Play && player.PlayStatus == PlayStatus.ActionPhase && instance is IAction a2)
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
            }
            else if (action == PlayerAction.React && instance is IReaction r)
            {
                r.ReactionEffect(this);
            }
            else if (action == PlayerAction.Buy)
            {                
                if (Supply.Contains(card) && instance.Cost <= player.MoneyPlayed && player.NumberOfBuys >= 1)
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
            else if (playerAction == PlayerAction.TakeAttackEffect && player.PlayStatus == PlayStatus.Responder)
            {
                var attacker = Players.Single(x => x.PlayStatus == PlayStatus.Attacker);
                var lastPlayedAttack = attacker.GetLastPlayedCard();
                
                if (lastPlayedAttack is IAttack attack)
                {
                    attack.AttackEffect(player, this);
                }

                player.PlayStatus = PlayStatus.WaitForTurn;
                
                if (nextPlayer == attacker)
                {
                    attacker.PlayStatus = attacker.HasActionInHand() ? PlayStatus.ActionPhase : PlayStatus.BuyPhase;
                }
                else
                {
                    nextPlayer.PlayStatus = PlayStatus.Responder;
                }
                

            }
            else if (playerAction == PlayerAction.EndActionPhase)
            {
                player.EndActionPhase();
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
            //TODO: this fails if there is a request from card that wasn't played
            //var instance = player.PlayedCards[player.PlayedCards.Count - 1];
            if (player.PlayedAbilities.Last().Resolved == false)
            {
                var aInstance = player.PlayedAbilities.Last();

                if (aInstance is IResponseRequired<ActionResponse> rr)
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

            var instance = player.PlayedCards.Last(x => x.Card.Name == player.ActionRequest.Requester).Card;

            //TODO: move to IResponseRequired<IEnumerable<Card>> interface
            if (actionRequestType == ActionRequestType.SelectCards && instance is ISelectCardsResponseRequired sc)
            {
                sc.ResponseReceived(this, cards);
                CheckPlayStack(player);
            }
            else if (actionRequestType == ActionRequestType.SelectCards && instance is IResponseRequired<IEnumerable<Card>> rr)
            {
                rr.ResponseReceived(this, cards);
                CheckPlayStack(player);
            }
        }

        public IPlayer GetNextPlayer(IPlayer currentPlayer)
        {
            var index = Players.IndexOf(currentPlayer);           

            return index >= Players.Count - 1 ? Players[0] : Players[index + 1];
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
            return Players.Single(x =>
                x.PlayStatus == PlayStatus.ActionPhase || x.PlayStatus == PlayStatus.BuyPhase
                || x.PlayStatus == PlayStatus.ActionRequestResponder);
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
