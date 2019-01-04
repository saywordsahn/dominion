namespace DominionWeb.Game.Cards.Filters
{
    public class TreasureFilter : ICardFilter
    {
        public bool Apply(ICard card) => card is ITreasure;
    }
}