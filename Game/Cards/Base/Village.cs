using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Base
{
    public class Village : ICard, IAction, IRulesHolder
    {
        public int Cost { get; } = 3;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Village;


        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            return new List<IRule>
            {
                new PlusActions(2),
                new PlusCards(1)
            };
        }
    }
}