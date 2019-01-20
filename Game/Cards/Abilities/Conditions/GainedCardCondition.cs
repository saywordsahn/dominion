using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.Conditions
{
    public class GainedCardCondition : IAbilityCondition
    {
        public ICardFilter CardFilter { get; set; }
        public ICard Card { get; set; }
        
        public GainedCardCondition(ICardFilter cardFilter, ICard card)
        {
            CardFilter = cardFilter;
            Card = card;
        }
        
        public bool IsMet(Game game, IPlayer player)
        {
            return CardFilter.Apply(Card);
        }
    }
}