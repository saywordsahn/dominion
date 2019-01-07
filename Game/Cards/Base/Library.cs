using System;
using DominionWeb.Game.Cards.Types;

namespace DominionWeb.Game.Cards.Base
{
    public class Library : ICard, IAction
    {
        public int Cost { get; } = 5;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Library;

        public void Resolve(Game game)
        {
            throw new NotImplementedException();
        }
    }
}