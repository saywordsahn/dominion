namespace DominionWeb.Game.Cards.Base
{
    public class Estate : ICard, IVictory
    {
        public int Cost { get; } = 2;

        public CardType CardType { get; } = CardType.Victory;

        public Card Name { get; } = Card.Estate;

        public int GetVictoryPointValue(IPlayer player)
        {
            return 1;
        }
    }
}