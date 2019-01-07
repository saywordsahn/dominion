using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Base
{
    public class Duchy : ICard, IVictory
    {
        public Card Name { get; } = Card.Duchy;

        public int Cost { get; } = 5;

        public CardType CardType { get; } = CardType.Victory;
        
        public int GetVictoryPointValue(IPlayer player)
        {
            return 3;
        }
    }
}