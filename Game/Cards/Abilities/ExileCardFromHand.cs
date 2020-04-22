using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class ExileCardFromHand : IAbility, IResponseRequired<ActionResponse>
    {
        public bool Resolved { get; set; }

        public bool CardExiled { get; set; } = false;

        public void Resolve(Game game, IPlayer player)
        {
            if (player.Hand.Contains(Card.Copper))
            {
                player.ActionRequest = new YesNoActionRequest(Card.Sanctuary, "Would you like to exile a card from your hand?");
                player.PlayStatus = PlayStatus.ActionRequestResponder;
            }
        }

        public void ResponseReceived(Game game, ActionResponse response)
        {
            var player = game.GetActivePlayer();

            if (response == ActionResponse.No || CardExiled)
            {
                player.PlayStatus = PlayStatus.ActionPhase;
                Resolved = true;
                return;
            }

            if (response == ActionResponse.Yes)
            {
                var selectableCards = player.Hand
                .Select(CardFactory.Create)
                .Select(x => x.Name)
                .ToList();

                if (selectableCards.Count == 0)
                {
                    player.PlayStatus = PlayStatus.ActionPhase;
                    Resolved = true;
                }
                else
                {
                    player.ActionRequest = new SelectCardsActionRequest("Select a card to exile.",
                        Card.Any, selectableCards, 1);
                    player.PlayStatus = PlayStatus.ActionRequestResponder;
                    Resolved = true;
                }
            }
        }

        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var player = game.GetActivePlayer();

            var cardList = response.ToList();

            if (cardList.Count == 1)
            {
                player.Hand.Remove(cardList[0]);
                player.ExileMat.Add(cardList[0]);

                player.PlayStatus = PlayStatus.ActionPhase;
                CardExiled = true;
                Resolved = true;
            }

        }


    }
}
