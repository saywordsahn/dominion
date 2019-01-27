using System.Collections.Generic;

namespace DominionWeb.Game.Supply
{
    public interface ISupply
    {
        IEnumerable<IPile> VictorySupply { get; }
        IEnumerable<IPile> TreasureSupply { get; }
        IEnumerable<IPile> KingdomSupply { get; }
        IEnumerable<Card> GetGainableCards();
        ICollection<Card> Trash { get; }
        void Return(Card card);
        Card Take(Card card);
        Card Take(SupplyType supplyType, Card card);
        bool Contains(Card card);
        bool CardIsVisible(Card card);
        bool Contains(Card card, int numberOfCards);
        void AddToTrash(Card card);
        bool NoProvincesRemain();
        bool ThreeOrMorePilesEmpty();
        int EmptyPileCount();
        bool IncludeRuins { get; set; }
        IPile RuinsPile { get; set; }
    }
}
