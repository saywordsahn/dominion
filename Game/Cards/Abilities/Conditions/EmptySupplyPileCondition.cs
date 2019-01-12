using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.Conditions
{
    public class EmptySupplyPileCondition : IAbilityCondition
    {
        public int NumberOfEmptyPiles { get; set; }

        public EmptySupplyPileCondition(int numberOfEmptyPiles)
        {
            NumberOfEmptyPiles = numberOfEmptyPiles;
        }


        public bool IsMet(Game game, IPlayer player)
        {
            return game.Supply.EmptyPileCount() >= NumberOfEmptyPiles;
        }
    }
}