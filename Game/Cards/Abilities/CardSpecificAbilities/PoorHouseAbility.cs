using System;
using System.Linq;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class PoorHouseAbility : IAbility
    {
        public bool Resolved { get; set; }

        public void Resolve(Game game, IPlayer player)
        {
            //TODO: reveal hand

            var treasureFilter = new TreasureFilter();

            var treasureCount = player.Hand.Select(CardFactory.Create).Count(treasureFilter.Apply);

            player.MoneyPlayed = Math.Max(0, player.MoneyPlayed - treasureCount);

            Resolved = true;
        }
    }
}
