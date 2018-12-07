using System;

namespace DominionWeb.Game.Cards.Base
{
    public class Bureaucrat : ICard, IAction, IAttack
    {
        public int Cost { get; } = 4;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Bureaucrat;

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