using System.Collections.Generic;

namespace DominionWeb.Game.Player
{
    public class TavernMat
    {
        public IList<Card> Cards { get; set; }

        public TavernMat()
        {
            Cards = new List<Card>();
        }
        
        public void Add(Card card)
        {
            Cards.Add(card);
        }
    }
}