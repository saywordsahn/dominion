using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Common;

namespace DominionWeb.Game.Cards.Base
{
    public class Poacher : ICard, IAction, IResponseRequired<IEnumerable<Card>>
    {
        public int Cost { get; } = 4;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Poacher;

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.Draw(1);
            player.NumberOfActions++;
            player.MoneyPlayed++;

            var emptyPileCount = game.Supply.EmptyPileCount();

            if (emptyPileCount > 0)
            {
                //discard a cardPerEmptyPile
                player.ActionRequest = new SelectCardsActionRequest("Discard " + emptyPileCount + " cards.",
                    Card.Poacher, player.Hand, emptyPileCount);
                player.PlayStatus = PlayStatus.ActionRequestResponder;
            }
            
        }

        public void ResponseReceived(Game game, IEnumerable<Card> cards)
        {
            var player = game.GetActivePlayer();

            var cardList = cards.ToList();

            if (cardList.Count != game.Supply.EmptyPileCount()) return;

            foreach (var c in cards)
            {
                player.DiscardFromHand(c);
            }
        }
    }
}