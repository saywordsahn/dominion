namespace DominionWeb.Game
{
    public interface IVictoryCondition
    {
        bool IsMet(ISupply supply);
    }
}