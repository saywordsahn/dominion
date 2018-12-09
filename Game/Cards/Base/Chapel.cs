using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Common;

namespace DominionWeb.Game.Cards.Base
{
    public class Chapel : ICard, IAction, ISelectCardsResponseRequired
    {
        public int Cost { get; } = 2;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Chapel;

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.ActionRequest = new SelectCardsActionRequest("You may trash up to 4 cards.", Card.Chapel,
                player.Hand, 4);
            player.PlayStatus = PlayStatus.ActionRequestResponder;
        }

        public void ResponseReceived(Game game, IEnumerable<Card> cards)
        {
            var player = game.GetActivePlayer();
            player.PlayStatus = PlayStatus.ActionPhase;

            foreach (var card in cards)
            {
                player.TrashFromHand(game.Supply, card);
            }
            

        }

    }
}