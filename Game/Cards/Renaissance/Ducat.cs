using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Renaissance
{
    public class Ducat : ICard, ITreasure, ITreasureAbilityHolder, IOnGainAbilityHolder
    {
        public Card Name { get; } = Card.Ducat;

        public CardType CardType { get; } = CardType.Treasure;

        public int Cost { get; } = 2;

        public int Value { get; } = 0;
        
        public PlayStatus PriorStatus { get; set; }

        public IAbility OnGainAbility { get; set; } = new TrashFromHandAbility();

        public void ResolveTreasureAbilities(IPlayer player)
        {
            player.NumberOfBuys++;
            player.Coffers++;
        }

    }
}