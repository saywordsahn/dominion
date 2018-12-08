namespace DominionWeb.Game.Supply
{
    public interface ISupply
    {
        Card Take(Card card);
        Card Take(SupplyType supplyType, Card card);
        bool Contains(Card card);
        void AddToTrash(Card card);
        bool NoProvincesRemain();
        bool ThreeOrMorePilesEmpty();
    }
}
