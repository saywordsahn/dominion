using System;
using DominionWeb.Game.Common;

namespace DominionWeb.Game.Cards.Base
{
    public class Cellar : ICard, IAction
    {
        public int Cost { get; } = 2;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Cellar;

        public void Resolve(Game game)
        {
            throw new NotImplementedException();
        }
    }
}