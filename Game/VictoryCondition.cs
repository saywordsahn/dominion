using DominionWeb.Game.Supply;

namespace DominionWeb.Game
{
    public class VictoryCondition : IVictoryCondition
    {
        public bool IsMet(ISupply supply)
        {
            return supply.NoProvincesRemain() || supply.ThreeOrMorePilesEmpty();
        }
    }
}