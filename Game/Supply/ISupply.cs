﻿using System.Collections;
using System.Collections.Generic;

namespace DominionWeb.Game.Supply
{
    public interface ISupply
    {
        IEnumerable<Pile> VictorySupply { get; }
        IEnumerable<Pile> TreasureSupply { get; }
        IEnumerable<Pile> KingdomSupply { get; }
        IEnumerable<Card> GetDistinctCards();
        Card Take(Card card);
        Card Take(SupplyType supplyType, Card card);
        bool Contains(Card card);
        void AddToTrash(Card card);
        bool NoProvincesRemain();
        bool ThreeOrMorePilesEmpty();
        int EmptyPileCount();
    }
}
