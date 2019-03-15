using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Requests;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Intrigue
{
    public class Nobles : ICard, IAction, IRulesHolder, IVictory
    {
        public Card Name { get; } = Card.Nobles;
        public int Cost { get; } = 6;
        public CardType CardType { get; } = CardType.Action;

        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            return new List<IRule>
            {
                new SelectAbilities(new List<RequestOptionAbility>
                {
                    new RequestOptionAbility(new RequestOption(ActionResponse.Draw, "+3 cards"), new PlusCards(3)),
                    new RequestOptionAbility(new RequestOption(ActionResponse.Action, "+2 actions"), new PlusActions(2)),
                }, 1)	
            };
        }

        public int GetVictoryPointValue(IPlayer player)
        {
            return 2;
        }
    }
}