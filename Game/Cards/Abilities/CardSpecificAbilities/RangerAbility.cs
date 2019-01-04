using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class RangerAbility : IAbility
    {
        public void Resolve(Game game, IPlayer player)
        {
            if (player.JourneyTokenIsFaceUp)
            {
                player.Draw(4);
            }

            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}