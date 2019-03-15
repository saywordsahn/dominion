using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Empires
{
    public class Sacrifice : ICard, IAction, IResponseRequired<IEnumerable<Card>>
    {
        public Card Name { get; } = Card.Sacrifice;
        public int Cost { get; } = 4;
        public CardType CardType { get; } = CardType.Action;

        //TODO: Implement
        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            throw new System.NotImplementedException();
        }

        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var currentPlayer = game.GetActivePlayer();
            
            var cardList = response.ToList();

            if (cardList.Count == 1)
            {
                var card = cardList[0];
                var instance = CardFactory.Create(card);

                if (instance is IAction)
                {
                    currentPlayer.Draw(2);
                    currentPlayer.NumberOfActions += 2;
                }

                if (instance is ITreasure)
                {
                    currentPlayer.MoneyPlayed += 2;
                }

                if (instance is IVictory)
                {
                    currentPlayer.VictoryTokens += 2;
                }

                currentPlayer.Hand.Remove(card);
                game.Supply.AddToTrash(card);
                

                currentPlayer.PlayStatus = PlayStatus.ActionPhase;
            }
        }
    }
}