using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Base
{
    public class Curse : ICard, ICurse
    {
        public int Cost { get; } = 0;

        public CardType CardType { get; } = CardType.Curse;

        public Card Name { get; } = Card.Curse;

        public int GetVictoryPointValue(IPlayer player)
        {
            return -1;
        }
    }
}