using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
	public class CellarAbility : IAbility, IResponseRequired<IEnumerable<Card>>
    {
        public bool Resolved { get; set; }

        public void Resolve(Game game, IPlayer player)
        {
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
                Resolved = true;
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
            Resolved = true;
        }
}
}
