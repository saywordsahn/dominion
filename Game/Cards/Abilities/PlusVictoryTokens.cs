using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class PlusVictoryTokens : IAbility
    {
        public int Amount { get; set; }

        public PlusVictoryTokens(int amount)
        {
            Amount = amount;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            player.VictoryTokens += Amount;
            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}