namespace DominionWeb.Game.Cards.Base
{
    public class Silver : ICard, ITreasure
    {
        private readonly Card _name = Card.Silver;
        private readonly CardType _cardType = CardType.Treasure;
        private readonly int _cost = 3;
        private readonly int _value = 2;

        public Card Name => _name;
        public CardType CardType => _cardType;
        public int Cost => _cost;
        public int Value => _value;
    }
}