namespace DominionWeb.Game.Cards.Base
{
    public class Duchy : ICard
    {
        private readonly Card _name = Card.Duchy;
        private readonly CardType _cardType = CardType.Victory;
        private int _cost = 5;

        public Card Name => _name;
        public int Cost => _cost;
        public CardType CardType => _cardType;
    }
}