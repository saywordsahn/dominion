using System;

namespace DominionWeb.Game.Cards.Base
{
    public class Festival : ICard, IAction
    {
        public int Cost { get; } = 5;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Festival;

        public void Resolve(Game game)
        {
            throw new NotImplementedException();
        }
    }
}