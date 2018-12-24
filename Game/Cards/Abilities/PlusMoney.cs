using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class PlusMoney : IAbility
    {
        public int Amount { get; set; }
        public bool Resolved { get; set; }

        public PlusMoney(int amount)
        {
            Amount = amount;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            player.MoneyPlayed += Amount;
            Resolved = true;
        }

    }
    
}