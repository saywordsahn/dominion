using System;

namespace DominionWeb.Game.Cards.Base
{
    //TODO: we'll need a new interface for special victory cards
    // or change the original interface
    public class Gardens : ICard
    {
        private readonly Card _name = Card.Gardens;
        private readonly CardType _cardType = CardType.Victory;
        private int _cost = 4;
    
        public int Cost => _cost;
        public CardType CardType => _cardType;
        public Card Name => _name;

    }
}