using System;
using DominionWeb.Game.Supply;

namespace DominionWeb.Game
{
    public interface ISupply
    {
        Card Take(Card card);
        Card Take(SupplyType supplyType, Card card);
        bool Contains(Card card);
        void AddToTrash(Card card);
    }
}
