using System;
using DominionWeb.Game.Cards.Types;

namespace DominionWeb.Game.Cards.Base
{
    public class Mine : ICard
    {
        public int Cost { get; } = 5;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Mine;
    }
}