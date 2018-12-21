using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Base
{
    public class Remodel : ICard, IAction, IResponseRequired<IEnumerable<Card>>
    {
        public int Cost { get; } = 4;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Remodel;

        public bool IsCardTrashed { get; set; } = false;
        public Card TrashedCard { get; set; }

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.ActionRequest = new SelectCardsActionRequest("Select a card to trash.",
                Card.Remodel, player.Hand, 1);
            player.PlayStatus = PlayStatus.ActionRequestResponder;
        }

        public void ResponseReceived(Game game, IEnumerable<Card> cards)
        {
            var player = game.GetActivePlayer();
            var cardList = cards.ToList();

            if (cardList.Count != 1) return;

            var instance = CardFactory.Create(cardList[0]);
            
            if (!IsCardTrashed)
            {
                TrashCard(game, instance, player);
            }
            else
            {
                var trashedCard = CardFactory.Create(TrashedCard);

                if (instance.Cost <= trashedCard.Cost + 2)
                {
                    game.Supply.Take(instance.Name);
                    player.Gain(instance.Name);
                    player.PlayStatus = PlayStatus.ActionPhase;
                }
            }
            
        }

        private void TrashCard(Game game, ICard card, IPlayer player)
        {
            player.TrashFromHand(game.Supply, card.Name);
            IsCardTrashed = true;
            TrashedCard = card.Name;

            var selectableCards = game.Supply.GetDistinctCards()
                .Select(CardFactory.Create)
                .Where(x => x.Cost <= card.Cost + 2)
                .Select(x => x.Name)
                .ToList();
            
            player.ActionRequest = new SelectCardsActionRequest("Select a card to gain.",
                Card.Remodel, selectableCards, 1);
        }
    }
}