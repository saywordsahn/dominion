using System.Collections.Generic;
using System.Linq;

namespace DominionWeb.Game.Supply
{
    public class Pile : IPile
    {
        public IList<Card> Cards { get; set; }

        public Pile()
        {
            Cards = new List<Card>();
        }
        
        public Pile(IList<Card> cards)
        {
            Cards = cards;
        }

        public Pile(Card card, int number)
            : this(Enumerable.Repeat(card, number).ToList())
        { }
    }
}