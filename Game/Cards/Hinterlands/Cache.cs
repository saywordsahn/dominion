using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Hinterlands
{
    public class Cache: ICard, ITreasure, IOnGainAbilityHolder
    {
        public Card Name { get; } = Card.Cache;
        public int Cost { get; } = 5;
        public CardType CardType { get; } = CardType.Treasure;
        public int Value { get; } = 3;
                
        public void ResolveOnGainAbilities(IPlayer player)
        {
            player.RuleStack.Push(new GainCard(Card.Copper, 2));
        }

        public void OnGain(IPlayer player)
        {            
            player.Gain(Card.Copper);
            player.Gain(Card.Copper);
        }
    }
}