using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Renaissance
{
    public class SilkMerchant : ICard, IAction, IRulesHolder, IOnGainAbilityHolder, IOnTrashAbilityHolder
    {
        public Card Name { get; } = Card.SilkMerchant;
        public int Cost { get; } = 4;
        public CardType CardType { get; } = CardType.Action;
        public void Resolve(Game game)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            return new List<IRule>
            {
                new PlusBuys(1),
                new PlusCards(2)
            };
        }
        
        public void ResolveOnGainAbilities(IPlayer player)
        {
            player.RuleStack.Push(new PlusVillagers(1));
            player.RuleStack.Push(new PlusCoffers(1));
        }

        public void ResolveOnTrashAbilities(IPlayer player)
        {
            player.RuleStack.Push(new PlusVillagers(1));
            player.RuleStack.Push(new PlusCoffers(1));
        }
    }
}