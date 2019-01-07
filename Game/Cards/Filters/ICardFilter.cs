using DominionWeb.Game.Cards.Types;

namespace DominionWeb.Game.Cards.Filters
{
    public interface ICardFilter
    {
        bool Apply(ICard card);
    }
}