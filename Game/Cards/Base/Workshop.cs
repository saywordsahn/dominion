using System;

namespace DominionWeb.Game.Cards.Base
{
    public class Workshop : ICard, IAction
    {
        public int Cost { get; } = 3;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Workshop;

        public void Resolve(Game game)
        {
            throw new NotImplementedException();
        }
    }
}