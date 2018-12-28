using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.DarkAges
{
    public class Spoils : ICard, ITreasure, ITreasureAbilityHolder
    {
        public Card Name { get; } = Card.Spoils;
        public int Cost { get; } = 0;
        public CardType CardType { get; } = CardType.Treasure;
        public int Value { get; } = 3;
        public void ResolveTreasureAbilities(IPlayer player)
        {
            player.Hand.Remove(Card.Spoils);
        }
    }
}