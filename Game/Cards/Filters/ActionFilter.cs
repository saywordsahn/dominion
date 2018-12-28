namespace DominionWeb.Game.Cards.Filters
{
    public class ActionFilter : ICardFilter
    {
        public bool Apply(ICard card) => card is IAction;
    }
}