using System.Linq;

namespace DominionWeb.Game.Supply
{
    public class SplitPile : Pile
    {
        public SplitPile(Card topHalf, Card bottomHalf)
            :base(Enumerable.Repeat(bottomHalf, 5).Concat(Enumerable.Repeat(topHalf, 5)).ToList())
        {
            
        }
    }
}