using System;

namespace DominionWeb.Game.Cards.Base
{
    public class Militia : ICard, IAction, IAttack
    {
        public int Cost { get; } = 4;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Militia;

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