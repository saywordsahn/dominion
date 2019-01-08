using DominionWeb.Game.Cards;
using DominionWeb.Game.Cards.Filters;

namespace DominionWeb.Game.Player.Triggers
{
    public class OnGainTrigger : ITrigger
    {
        public ICardFilter Filter { get; set; }

        public OnGainTrigger(ICardFilter filter)
        {
            Filter = filter;
        }

        public bool IsMet(PlayerAction playerAction, Card card)
        {
            var instance = CardFactory.Create(card);
            return playerAction == PlayerAction.Gain && Filter.Apply(instance);
        }
    }
}