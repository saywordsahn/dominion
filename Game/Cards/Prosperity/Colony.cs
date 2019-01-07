using DominionWeb.Game.Cards.Types;

namespace DominionWeb.Game.Cards.Prosperity
{
    public class Colony : ICard
    {
        public Card Name { get; }
        public int Cost { get; }
        public CardType CardType { get; }
    }
}