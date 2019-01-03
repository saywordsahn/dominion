using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Common;

namespace DominionWeb.Game.Cards.Base
{
    public class ThroneRoom : ICard, IAction, IResponseRequired<IEnumerable<Card>>
    {
        public int Cost { get; } = 4;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.ThroneRoom;

        public Card CardSelected { get; private set; }
        
        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            var cards = player.Hand
                .Select(CardFactory.Create)
                .Where(x => x is IAction)
                .Select(x => x.Name)
                .ToList();

            if (cards.Count > 0)
            {
                player.ActionRequest = new SelectCardsActionRequest("Select a card to play twice.",
                    Card.ThroneRoom, cards, 1);
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

            if (cardList.Count != 1) return;
            
            var instance = CardFactory.Create(cardList[0]);

            if (instance is IAction a)
            {
                CardSelected = cardList[0];
                player.PlayWithoutCost(instance);

                player.PlayStack.Push(new PlayedCard(CardFactory.Create(cardList[0], true), true));
                player.PlayStack.Push(new PlayedCard(instance, false));
                player.PlayStatus = PlayStatus.ActionPhase;
            }

        }
    }
}