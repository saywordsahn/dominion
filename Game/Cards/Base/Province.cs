namespace DominionWeb.Game.Cards.Base
{
    public class Province : ICard, IVictory
    {
        public Card Name { get; } = Card.Province;

        public int Cost { get; } = 8;

        public CardType CardType { get; } = CardType.Victory;

        public int GetVictoryPointValue(Game game)
        {
            return 6;
        }
    } 
}