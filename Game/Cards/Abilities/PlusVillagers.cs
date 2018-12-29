using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class PlusVillagers : IAbility
    {
        public bool Resolved { get; set; }
        public int Amount { get; set; }

        public PlusVillagers(int amount)
        {
            Amount = amount;
        }
        public void Resolve(Game game, IPlayer player)
        {
            //TODO: consider redoing resolve 
            player.Villagers += Amount;
            Resolved = true;
        }

    }
}