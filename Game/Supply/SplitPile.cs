using System.Collections.Generic;
using System.Linq;

namespace DominionWeb.Game.Supply
{
    public class SplitPile : IPile
    {
        
        public SplitPile(Card topHalf, Card bottomHalf)
        {
            Cards = Enumerable.Repeat(bottomHalf, 5).Concat(Enumerable.Repeat(topHalf, 5)).ToList();
        }

        public IList<Card> Cards { get; set; }
    }
}