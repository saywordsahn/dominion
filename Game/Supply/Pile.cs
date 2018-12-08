using System.Collections.Generic;

namespace DominionWeb.Game.Supply
{
    public class Pile
    {
        public IList<Card> Cards;

        public Pile(IList<Card> cards)
        {
            Cards = cards;
        }
    }
}