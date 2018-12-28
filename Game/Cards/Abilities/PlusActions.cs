using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class PlusActions : IAbility
    {
        public int Amount { get; set; }

        public PlusActions(int amount)
        {
            Amount = amount;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            player.NumberOfActions += Amount;
            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}