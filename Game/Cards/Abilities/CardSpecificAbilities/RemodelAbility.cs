using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
	public class RemodelAbility : IAbility, IResponseRequired<IEnumerable<Card>>
    {
        public bool Resolved { get; set; }
        public bool IsCardTrashed { get; set; } = false;
        public Card TrashedCard { get; set; }
        public int GainAmount { get; set; }

        public RemodelAbility(int gainAmount = 2)
        {
            GainAmount = gainAmount;
        }

        public void Resolve(Game game, IPlayer player)
        {
            player.ActionRequest = new SelectCardsActionRequest("Select a card to trash.",
                Card.Remodel, player.Hand, 1);
            player.PlayStatus = PlayStatus.ActionRequestResponder;
        }

        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var player = game.GetActivePlayer();
            var cardList = response.ToList();

            if (cardList.Count != 1) return;

            var instance = CardFactory.Create(cardList[0]);

            if (!IsCardTrashed)
            {
                TrashCard(game, instance, player);
            }
            else
            {
                var trashedCard = CardFactory.Create(TrashedCard);

                if (instance.Cost <= trashedCard.Cost + GainAmount)
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

            var selectableCards = game.Supply.GetGainableCards()
                .Select(CardFactory.Create)
                .Where(x => x.Cost <= card.Cost + GainAmount)
                .Select(x => x.Name)
                .ToList();

            player.ActionRequest = new SelectCardsActionRequest("Select a card to gain.",
                Card.Remodel, selectableCards, 1);
        }
    }
}
