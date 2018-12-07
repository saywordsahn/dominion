namespace DominionWeb.Game.Cards.Base
{
    public class Copper : ICard, ITreasure
    {
        private readonly Card _name = Card.Copper;
        private readonly CardType _cardType = CardType.Treasure;
        private readonly int _cost = 0;
        private readonly int _value = 1;

        public Card Name => _name;
        public CardType CardType => _cardType;
        public int Cost => _cost;
        public int Value => _value;
    }
}