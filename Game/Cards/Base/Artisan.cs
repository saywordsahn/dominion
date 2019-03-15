using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Base
{
    public class Artisan : ICard, IAction, IRulesHolder
    {
        public int Cost { get; } = 6;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Artisan;

        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            return new List<IRule>
            {
                new PutCardOnDeck(),
                new GainCardCostingUpToX(5, GainTarget.Hand)
            };
        }


    }
}