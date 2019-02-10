using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class HarbingerAbility : IAbility, IResponseRequired<IEnumerable<Card>>
    {
        public bool Resolved { get; set; }

        public void Resolve(Game game, IPlayer player)
        {
            //TODO: add minCardsSelectable
            player.ActionRequest = new SelectCardsActionRequest("You may select a card to put on top of your deck.",
                Card.Harbinger, player.DiscardPile, 1);
            player.PlayStatus = PlayStatus.ActionRequestResponder;
        }

        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var player = game.GetActivePlayer();

            var cardList = response.ToList();

            if (cardList.Count != 1) return;

            var card = cardList.First();

            player.DiscardPile.Remove(card);
            player.Deck.Add(card);

            player.PlayStatus = PlayStatus.ActionPhase;
            Resolved = true;
        }
    }
}
