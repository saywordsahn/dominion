namespace DominionWeb.Game.Supply
{
    public interface ISupplyFactory
    {
        Supply Create(int numberOfPlayers);
    }
}