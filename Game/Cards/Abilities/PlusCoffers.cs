using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class PlusCoffers : IAbility
    {
        public bool Resolved { get; set; }
        public int Amount { get; set; }

        public PlusCoffers(int amount)
        {
            Amount = amount;
        }
        public void Resolve(Game game, IPlayer player)
        {
            player.Coffers += Amount;
            Resolved = true;
        }

    }
}