using System.Collections.Generic;
using System.Linq;

namespace DominionWeb.Game.Supply
{
    public class Pile : IPile
    {
        public Card PileCard { get; set; }
        public IList<Card> Cards { get; set; }

        public Pile()
        {
            Cards = new List<Card>();
        }
        
        public Pile(IList<Card> cards)
        {
            PileCard = cards.Last();
            Cards = cards;
        }
        
        public Pile(Card pileCard, IList<Card> cards)
        {
            PileCard = pileCard;
            Cards = cards;
        }

        public Pile(Card card, int number)
            : this(card, Enumerable.Repeat(card, number).ToList())
        { }
    }
}