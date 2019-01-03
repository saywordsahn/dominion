using System.Collections.Generic;
using System.Linq;

namespace DominionWeb.Game.Supply
{
    public class SplitPile : IPile
    {
        
        public SplitPile(Card pileCard, Card topHalf, Card bottomHalf)
        {
            PileCard = pileCard;
            Cards = Enumerable.Repeat(bottomHalf, 5).Concat(Enumerable.Repeat(topHalf, 5)).ToList();
        }

        public Card PileCard { get; set; }
        public IList<Card> Cards { get; set; }
    }
}