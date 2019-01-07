using DominionWeb.Game.Cards.Types;

namespace DominionWeb.Game.Cards.Filters
{
    public class VictoryFilter : ICardFilter
    {
        public bool Apply(ICard card) => card is IVictory;
    }
}