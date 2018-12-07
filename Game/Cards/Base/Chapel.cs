using System;

namespace DominionWeb.Game.Cards.Base
{
    public class Chapel : ICard, IAction
    {
        private readonly Card _name = Card.Chapel;
        private readonly CardType _cardType = CardType.Action;
        private int _cost = 2;
    
        public int Cost => _cost;
        public CardType CardType => _cardType;
        public Card Name => _name;

        public void Resolve(Game game)
        {
            throw new NotImplementedException();
        }
    }
}