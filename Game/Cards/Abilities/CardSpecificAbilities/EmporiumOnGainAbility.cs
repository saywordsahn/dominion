using System.Linq;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class EmporiumOnGainAbility : IAbility
    {
        public bool Resolved { get; set; }

        public void Resolve(Game game, IPlayer player)
        {
            var actionCount = player.PlayedCards
                .Select(x => x.Card)
                .Count(x => x is IAction);

            if (actionCount >= 5)
            {
                player.VictoryTokens += 2;
            }

        }

    }
}