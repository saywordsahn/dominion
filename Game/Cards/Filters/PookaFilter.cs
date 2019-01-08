using DominionWeb.Game.Cards.Types;

namespace DominionWeb.Game.Cards.Filters
{
    public class PookaFilter : ICardFilter
    {
        public bool Apply(ICard card)
        {
            return card is ITreasure && card.Name != Card.CursedGold;
        }
    }
}