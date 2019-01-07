using DominionWeb.Game.Cards.Types;

namespace DominionWeb.Game.Cards.Base
{
    public class Gold : ICard, ITreasure
    {
        public CardType CardType { get; } = CardType.Treasure;

        public int Cost { get; } = 6;

        public int Value { get; } = 3;

        public Card Name { get; } = Card.Gold;
    }
}