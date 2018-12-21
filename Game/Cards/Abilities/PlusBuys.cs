using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class PlusBuys : IAbility
    {
        public int Amount { get; set; }
        public bool Resolved { get; set; }

        public PlusBuys(int amount)
        {
            Amount = amount;
        }
        
        public void Resolve(IPlayer player)
        {
            player.NumberOfBuys += Amount;
            Resolved = true;
        }
    }
}