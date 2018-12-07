namespace DominionWeb.Game.Cards.Base
{
    public class Estate : ICard
    {
        private readonly Card _name = Card.Estate;
        private readonly CardType _cardType = CardType.Victory;
        private int _cost = 2;

        public int Cost => _cost;
        public CardType CardType => _cardType;

        public Card Name => _name;
    }
}