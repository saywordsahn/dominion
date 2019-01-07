using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Renaissance
{
    public class Lackeys : ICard, IAction, IRulesHolder, IOnGainAbilityHolder
    {
        public Card Name { get; } = Card.Lackeys;
        public int Cost { get; } = 2;
        public CardType CardType { get; } = CardType.Action;
        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            return new List<IRule> {new PlusCards(2)};
        }
        
        public void ResolveOnGainAbilities(IPlayer player)
        {
            player.RuleStack.Push(new PlusVillagers(2));
        }

        public void Resolve(Game game)
        {
            throw new System.NotImplementedException();
        }
    }
}