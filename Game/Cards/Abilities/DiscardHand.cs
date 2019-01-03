using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class DiscardHand : IAbility
    {
        public void Resolve(Game game, IPlayer player)
        {
            player.DiscardPile.AddRange(player.Hand);
            player.Hand.Clear();
            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}