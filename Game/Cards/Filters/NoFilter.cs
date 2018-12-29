namespace DominionWeb.Game.Cards.Filters
{
    public class NoFilter : ICardFilter
    {
        public bool Apply(ICard card) => true;
    }
}