using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.DarkAges
{
    public class RuinedMarket : ICard, IAction, IRulesHolder, IRuins
    {
        public Card Name { get; } = Card.RuinedMarket;
        public int Cost { get; } = 0;
        public CardType CardType { get; } = CardType.Action;

        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            return new List<IRule> {new PlusBuys(1)};
        }
    }
}