using DominionWeb.Game.Cards.Types;

namespace DominionWeb.Game.Cards.Base
{
    public class Silver : ICard, ITreasure
    {
        public Card Name { get; } = Card.Silver;

        public CardType CardType { get; } = CardType.Treasure;

        public int Cost { get; } = 3;

        public int Value { get; } = 2;
    }
}