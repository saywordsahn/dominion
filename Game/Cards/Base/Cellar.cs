using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Common;

namespace DominionWeb.Game.Cards.Base
{
    public class Cellar : ICard, IAction, ISelectCardsResponseRequired
    {
        public int Cost { get; } = 2;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Cellar;

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.NumberOfActions++;

            if (player.Hand.Count > 0)
            {
                player.ActionRequest = new SelectCardsActionRequest("Select any number of cards to discard, +1 card for each.",
                    Card.Cellar, player.Hand, player.Hand.Count);
                player.PlayStatus = PlayStatus.ActionRequestResponder;
            }
            else
            {
                player.PlayStatus = PlayStatus.ActionPhase;
            }
        }

        public void ResponseReceived(Game game, IEnumerable<Card> cards)
        {
            var player = game.GetActivePlayer();
            var cardList = cards.ToList();

            if (cardList.Count > 0)
            {
                foreach (var c in cardList)
                {
                    player.DiscardFromHand(c);
                }
                
                player.Draw(cardList.Count); 
            }

            player.PlayStatus = PlayStatus.ActionPhase;
        }
    }
}