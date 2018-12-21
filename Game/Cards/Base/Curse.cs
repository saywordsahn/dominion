using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Base
{
    public class Curse : ICard, IVictory
    {
        public int Cost { get; } = 0;

        public CardType CardType { get; } = CardType.Victory;

        public Card Name { get; } = Card.Curse;

        public int GetVictoryPointValue(IPlayer player)
        {
            return -1;
        }
    }
}