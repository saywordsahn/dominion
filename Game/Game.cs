using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DominionWeb.Game.Cards;
using DominionWeb.Game.Cards.Base;
using DominionWeb.Game.Common;
using DominionWeb.Game.Supply;
using Newtonsoft.Json;

namespace DominionWeb.Game
{
    public class Game
    {
        public readonly int GameId;
        public IList<IPlayer> Players { get; private set; }
        public ISupply Supply { get; private set; }

        private readonly IVictoryCondition _victoryCondition;

        public Game(int gameId, IList<IPlayer> players, ISupply supply, IVictoryCondition victoryCondition)
        {
            GameId = gameId;
            Players = players;
            Supply = supply;
            _victoryCondition = victoryCondition;
        }

        public void Initialize()
        {
            bool first = true;
            
            foreach (var player in Players)
            {
                var coppers = Enumerable.Repeat(Supply.Take(SupplyType.Treasure, Card.Copper), 7);
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

        public void Submit(string playerName, PlayerAction action, Card card)
        {
            var player = Players.Single(x => x.PlayerName == playerName);
            var cardInstance = CardFactory.Create(card);
            
            //TODO: consider combining ActionAttack into one status/interface
            if (action == PlayerAction.Play && cardInstance is IAction a1 && cardInstance is IAttack att)
            {
                player.Play(card);
                a1.Resolve(this);
            }
            else if (action == PlayerAction.Play && cardInstance is IAction a2)
            {
                player.Play(card);
                a2.Resolve(this);
                //TODO: look into refactoring this to player object (ex. player.SetActionOrBuyPhase())
                //TODO: check for number of actions remaining to move statuses automatically
                if (player.PlayStatus != PlayStatus.ActionRequestResponder)
                {
                    player.PlayStatus = player.HasActionInHand() ? PlayStatus.ActionPhase : PlayStatus.BuyPhase;
                }
                
            }
            else if (action == PlayerAction.Play && cardInstance is ITreasure t)
            {
                //we could implement by as an IAction and then we could remove this section
                //maybe a different abstraction is better though
                player.Play(card);
            }
            //TODO: refactor this as an admin privilege
            else if (action == PlayerAction.GainToHand && player.PlayerName == "ben@gmail.com")
            {
                player.GainToHand(card);
            }
            else if (action == PlayerAction.React && cardInstance is IReaction r)
            {
                r.ReactionEffect(this);
            }
            else if (action == PlayerAction.Buy)
            {                
                if (Supply.Contains(card) && cardInstance.Cost <= player.MoneyPlayed && player.NumberOfBuys >= 1)
                {
                    player.Buy(card);
                    Supply.Take(card);
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
                
                if (_victoryCondition.IsMet(Supply))
                {
                    //Game over
                    //determine winner
                }
                else
                {
                    nextPlayer.StartTurn();
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

        public void Submit(string playerName, ActionRequestType actionRequestType, ActionResponse actionResponse)
        {
            var player = Players.Single(x => x.PlayerName == playerName);
            var card = player.ActionRequest.Requester;

            //TODO: rethink this through, likely bugs, if not now, in future when more cards are added
            var instance = player.PlayedCards[player.PlayedCards.Count - 1];

            if (actionRequestType == ActionRequestType.YesNo && instance is IActionRequester ar)
            {
                ar.ResponseReceived(this, actionResponse);
            }
        }

        public void Submit(string playerName, ActionRequestType actionRequestType, IEnumerable<Card> cards)
        {
            var player = Players.Single(x => x.PlayerName == playerName);
            var card = player.ActionRequest.Requester;

            var instance = player.PlayedCards[player.PlayedCards.Count - 1];

            if (actionRequestType == ActionRequestType.SelectCards && instance is ISelectCardsResponseRequired sc)
            {
                sc.ResponseReceived(this, cards);
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
