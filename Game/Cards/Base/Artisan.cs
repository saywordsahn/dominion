using System;

namespace DominionWeb.Game.Cards.Base
{
    public class Artisan : ICard, IAction
    {
        public int Cost { get; } = 5;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Artisan;

        public void Resolve(Game game)
        {
            throw new NotImplementedException();
        }
    }
}