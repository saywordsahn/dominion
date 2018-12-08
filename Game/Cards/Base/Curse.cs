namespace DominionWeb.Game.Cards.Base
{
    public class Curse : ICard
    {
        public int Cost { get; } = 0;

        public CardType CardType { get; } = CardType.Victory;

        public Card Name { get; } = Card.Curse;
    }
}