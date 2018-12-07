using System;

namespace DominionWeb.Game.Cards.Base
{
    public class Remodel : ICard, IAction
    {
        public int Cost { get; } = 4;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Remodel;

        public void Resolve(Game game)
        {
            throw new NotImplementedException();
        }
    }
}