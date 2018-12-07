namespace DominionWeb.Game.Cards.Base
{
    public class Province : ICard
    {
        private readonly Card _name = Card.Province;
        private readonly CardType _cardType = CardType.Victory;
        private int _cost = 8;

        public Card Name => _name;
        public int Cost => _cost;
        public CardType CardType => _cardType;
    } 
}