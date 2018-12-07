using System;

namespace DominionWeb.Game.Cards.Base
{
    public class Poacher : ICard, IAction
    {
        public int Cost { get; } = 4;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Poacher;

        public void Resolve(Game game)
        {
            throw new NotImplementedException();
        }
    }
}