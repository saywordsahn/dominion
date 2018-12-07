namespace DominionWeb.Game.Cards.Base
{
    public class Gold : ICard, ITreasure
    {
        private readonly Card _name = Card.Gold;
        private readonly CardType _cardType = CardType.Treasure;
        private readonly int _cost = 6;
        private readonly int _value = 3;

        public CardType CardType => _cardType;
        public int Cost => _cost;
        public int Value => _value;
        public Card Name => _name;
    }
}