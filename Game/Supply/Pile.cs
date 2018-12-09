using System.Collections.Generic;

namespace DominionWeb.Game.Supply
{
    public class Pile
    {
        public readonly IList<Card> Cards;

        public Pile(IList<Card> cards)
        {
            Cards = cards;
        }
    }
}