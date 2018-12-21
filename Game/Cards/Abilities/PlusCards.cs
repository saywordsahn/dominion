using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class PlusCards : IAbility
    {
        public int Amount { get; set; }
        public bool Resolved { get; set; }

        public PlusCards(int amount)
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