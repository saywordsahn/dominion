using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Empires
{
    public class Plunder : ICard, ITreasure, ITreasureAbilityHolder
    {
        public Card Name { get; } = Card.Plunder;
        public int Cost { get; } = 5;
        public CardType CardType { get; } = CardType.Treasure;
        public int Value { get; } = 2;
        public void ResolveTreasureAbilities(IPlayer player)
        {
            player.VictoryTokens++;
        }
    }
}