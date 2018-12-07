using System;

namespace DominionWeb.Game.Cards.Base
{
    public class Militia : ICard, IAction, IAttack
    {
        private readonly Card _name = Card.Militia;
        private readonly CardType _cardType = CardType.Action;
        private int _cost = 4;
    
        public int Cost => _cost;
        public CardType CardType => _cardType;
        public Card Name => _name;

        public void Resolve(Game game)
        {
            throw new NotImplementedException();
        }

        public void AttackEffect(IPlayer attackedPlayer, Game game)
        {
            throw new NotImplementedException();
        }
    }
}