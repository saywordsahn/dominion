using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.CardSpecificAbilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Empires
{
    public class Emporium : ICard, IAction, IRulesHolder, IOnGainAbilityHolder
    {
        public Card Name { get; } = Card.Emporium;
        public int Cost { get; } = 5;
        public CardType CardType { get; } = CardType.Action;
        public void Resolve(Game game)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            return new List<IRule>
            {
                new PlusMoney(1),
                new PlusActions(1),
                new PlusCards(1)
            };
        }
        
        public void ResolveOnGainAbilities(IPlayer player)
        {
            player.RuleStack.Push(new EmporiumOnGainAbility());
        }
    }
}