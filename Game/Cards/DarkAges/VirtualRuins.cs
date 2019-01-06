namespace DominionWeb.Game.Cards.DarkAges
{
    public class VirtualRuins : ICard
    {
        public Card Name { get; } = Card.VirtualRuins;
        public int Cost { get; } = 0;
        public CardType CardType { get; } = CardType.Virtual;
    }
}