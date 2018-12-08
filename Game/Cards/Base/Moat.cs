using System;

namespace DominionWeb.Game.Cards.Base
{
    public class Moat : ICard, IAction
    {
        public int Cost { get; } = 2;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Moat;

        public void Resolve(Game game)
        {
            throw new NotImplementedException();
        }
    }
}