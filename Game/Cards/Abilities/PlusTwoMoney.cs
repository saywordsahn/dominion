using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class PlusTwoMoney : IAbility
    {
        public bool Resolved { get; set; }

        public void Resolve(IPlayer player)
        {
            player.MoneyPlayed += 2;
            Resolved = true;
        }
    }
}