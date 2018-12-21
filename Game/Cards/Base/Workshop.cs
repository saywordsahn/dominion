using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Common;

namespace DominionWeb.Game.Cards.Base
{
    public class Workshop : ICard, IAction, IResponseRequired<IEnumerable<Card>>
    {
        public int Cost { get; } = 3;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Workshop;
        
        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();

            var selectableCards = game.Supply.GetDistinctCards()
                .Select(CardFactory.Create)
                .Where(x => x.Cost <= 4)
                .Select(x => x.Name)
                .ToList();

            if (selectableCards.Count == 0)
            {
                player.PlayStatus = PlayStatus.ActionPhase;
            }
            else
            {
                player.ActionRequest = new SelectCardsActionRequest("Gain a card costing up to 4.",
                    Card.Workshop, selectableCards, 1);
                player.PlayStatus = PlayStatus.ActionRequestResponder;
            }
            
        }

        public void ResponseReceived(Game game, IEnumerable<Card> cards)
        {
            var player = game.GetActivePlayer();
            
            var cardList = cards.ToList();

            if (cardList.Count == 1)
            {
                player.PlayStatus = PlayStatus.ActionPhase;

                var card = cardList.First();

                game.Supply.Take(card);
                player.Gain(card);
            }
            
        }
    }
}