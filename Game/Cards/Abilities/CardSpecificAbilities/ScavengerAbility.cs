using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
	public class ScavengerAbility : IAbility, IResponseRequired<IEnumerable<Card>>
    {
		public bool Resolved { get; set; }

        public void Resolve(Game game, IPlayer player)
        {
            new SelectCardFromDiscard(new NoFilter(), "Select a card from your discard pile to topdeck.", true)
                .Resolve(game, player);
        }

        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var player = game.GetActivePlayer();
            var cardList = response.ToList();

            if (cardList.Count == 1)
            {
                player.DiscardPile.Remove(cardList[0]);
                player.Deck.Add(cardList[0]);
                player.PlayStatus = PlayStatus.ActionPhase;
                Resolved = true;
            }
        }
    }
}
