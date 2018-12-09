using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Common;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Internal;

namespace DominionWeb.Game.Cards.Base
{
    public class Harbinger : ICard, IAction, ISelectCardsResponseRequired
    {
        public int Cost { get; } = 3;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Harbinger;

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            //TODO: add minCardsSelectable
            player.ActionRequest = new SelectCardsActionRequest("You may select a card to put on top of your deck.",
                Card.Harbinger, player.DiscardPile, 1);
            player.PlayStatus = PlayStatus.ActionRequestResponder;
        }

        public void ResponseReceived(Game game, IEnumerable<Card> cards)
        {
            var player = game.GetActivePlayer();
            player.PlayStatus = PlayStatus.ActionPhase;

            var cardList = cards.ToList();

            if (cardList.Count != 1) return;

            var card = cardList.First();

            player.DiscardPile.Remove(card);
            player.Deck.Add(card);
            
        }
    }
}