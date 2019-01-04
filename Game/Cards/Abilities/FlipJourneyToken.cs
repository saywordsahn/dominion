using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class FlipJourneyToken : IAbility
    {
        public void Resolve(Game game, IPlayer player)
        {
            player.JourneyTokenIsFaceUp = !player.JourneyTokenIsFaceUp;
            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}