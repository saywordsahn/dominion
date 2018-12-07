using System;

namespace DominionWeb.Game.Cards.Base
{
    public class Workshop : ICard, IAction
    {
        private readonly Card _name = Card.Workshop;
        private readonly CardType _cardType = CardType.Action;
        private int _cost = 3;
    
        public int Cost => _cost;
        public CardType CardType => _cardType;
        public Card Name => _name;

        public void Resolve(Game game)
        {
            throw new NotImplementedException();
        }
    }
}