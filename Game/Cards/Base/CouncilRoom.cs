using System;

namespace DominionWeb.Game.Cards.Base
{
    public class CouncilRoom : ICard, IAction
    {
        public int Cost { get; } = 5;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.CouncilRoom;

        public void Resolve(Game game)
        {
            throw new NotImplementedException();
        }
    }
}