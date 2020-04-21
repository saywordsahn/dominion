using System;
using System.Collections.Generic;

namespace DominionWeb.Game.Player
{
    public class ExileMat
    {
        public IList<Card> Cards { get; set; }

        public ExileMat()
        {
            Cards = new List<Card>();
        }

        public void Add(Card card)
        {
            Cards.Add(card);
        }
    }
}
