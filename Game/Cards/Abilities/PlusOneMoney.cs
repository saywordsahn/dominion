using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    //TODO: refactor as PlusMoney
    public class PlusOneMoney : IAbility
    {
        public bool Resolved { get; set; }

        public void Resolve(IPlayer player)
        {
            player.MoneyPlayed++;
            Resolved = true;
        }

    }
}