namespace DominionWeb.Game.Cards.Base
{
    public class Curse : ICard
    {
        private readonly Card _name = Card.Curse;
        private readonly CardType _cardType = CardType.Victory;
        private int _cost = 0;

        public int Cost => _cost;
        public CardType CardType => _cardType;

        public Card Name => _name;
    }
}