using System;

namespace DominionWeb.Game.Cards.Base
{
    //TODO: we'll need a new interface for special victory cards
    // or change the original interface
    public class Gardens : ICard
    {
        public int Cost { get; } = 4;

        public CardType CardType { get; } = CardType.Victory;

        public Card Name { get; } = Card.Gardens;
    }
}