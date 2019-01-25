using System;
using System.Linq;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
	public class ForagerAbility : IAbility
    {

        public int GoldPerTreasure { get; set; }
        public bool Resolved { get; set; }

        public ForagerAbility(int goldPerTreasure = 1)
        {
            GoldPerTreasure = goldPerTreasure;
        }

        public void Resolve(Game game, IPlayer player)
        {
            var treasureFilter = new TreasureFilter();

            var treasuresInTrash = game
                .Supply.Trash
                .Select(CardFactory.Create)
                .Where(treasureFilter.Apply)
                .Select(x => x.Name)
                .Distinct()
                .ToList();

            player.MoneyPlayed += treasuresInTrash.Count * GoldPerTreasure;

            Resolved = true;
        }
    }
}
