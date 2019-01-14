using DominionWeb.Game.Cards;
using DominionWeb.Game.Cards.Filters;

namespace DominionWeb.Game.Player.Triggers
{
    //TODO: refactor triggers to OnActionTrigger?
    public class OnBuyTrigger : ITrigger
    {
        public ICardFilter Filter { get; set; }

        public OnBuyTrigger(ICardFilter filter)
        {
            Filter = filter;
        }
        
        public bool IsMet(PlayerAction playerAction, Card card)
        {
            var instance = CardFactory.Create(card);
            return playerAction == PlayerAction.Buy && Filter.Apply(instance);
        }
    }
}