using System.Collections;
using System.Collections.Generic;

namespace DominionWeb.Game.Supply
{
    public interface ISupply
    {
        IEnumerable<Card> GetDistinctCards();
        Card Take(Card card);
        Card Take(SupplyType supplyType, Card card);
        bool Contains(Card card);
        void AddToTrash(Card card);
        bool NoProvincesRemain();
        bool ThreeOrMorePilesEmpty();
    }
}
