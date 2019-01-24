using System;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
	public class DiscardDeck : IAbility
    {
        public bool Resolved { get; set; }

        public void Resolve(Game game, IPlayer player)
        {
            player.DiscardPile.AddRange(player.Deck);

            player.Deck.Clear();

            Resolved = true;
        }
    }
}
