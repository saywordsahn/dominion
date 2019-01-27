using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class PutCardOnDeck : IAbility, IResponseRequired<IEnumerable<Card>>
    {
        public bool Resolved { get; set; }

        public void Resolve(Game game, IPlayer player)
        {
            new SelectCardFromHand(new NoFilter(), "Select a card to topdeck")
                .Resolve(game, player);
        }

        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var player = game.GetActivePlayer();

            var cardList = response.ToList();

            if (cardList.Count == 1 && player.Hand.Contains(cardList[0]))
            {
                player.Hand.Remove(cardList[0]);
                player.Deck.Add(cardList[0]);
                player.PlayStatus = PlayStatus.ActionPhase;
                Resolved = true;
            }

        }
    }
}
