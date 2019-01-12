using DominionWeb.Game.Cards.Types;

namespace DominionWeb.Game.Cards.Filters
{
    //TODO: make this more elegant, maybe with fluent ui
    public class OrFilter : ICardFilter
    {
        public ICardFilter FilterA { get; set; }
        public ICardFilter FilterB { get; set; }

        public OrFilter(ICardFilter filterA, ICardFilter filterB)
        {
            FilterA = filterA;
            FilterB = filterB;
        }
        
        public bool Apply(ICard card)
        {
            return FilterA.Apply(card) || FilterB.Apply(card);
        }
    }
}