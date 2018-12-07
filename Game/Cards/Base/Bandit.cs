using System;

namespace DominionWeb.Game.Cards.Base
{
    public class Bandit : ICard, IAction, IAttack
    {
        public int Cost { get; } = 5;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Bandit;

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