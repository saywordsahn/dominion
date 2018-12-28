using System;

namespace DominionWeb.Game.GameComponents
{
    public class SpoilsPile
    {
        public readonly int MaxAmount;
        //TODO: eventually remove setter when we move from default JsonSerialization
        public int Count { get; set; }
        
        public SpoilsPile(int maxAmount)
        {
            MaxAmount = maxAmount;
            Count = maxAmount;
        }

        public Card Take()
        {
            if (Count <= 0) throw new InvalidOperationException("There are no spoils to take.");

            Count--;
            return Card.Spoils;
        }

        public void Add()
        {
            if (Count >= MaxAmount) throw new InvalidOperationException("There are too many Spoils in the game.");
            Count++;
        }
        
    }
}