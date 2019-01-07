using DominionWeb.Game.Cards.Types;

namespace DominionWeb.Game.Cards.Base
{
    public class Copper : ICard, ITreasure
    {
        public Card Name { get; } = Card.Copper;

        public CardType CardType { get; } = CardType.Treasure;

        public int Cost { get; } = 0;

        public int Value { get; } = 1;
    }
}