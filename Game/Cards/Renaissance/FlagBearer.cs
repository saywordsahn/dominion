using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.GameComponents.Artifacts;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Renaissance
{
    public class FlagBearer : ICard, IAction, IRulesHolder, IOnGainAbilityHolder, IOnTrashAbilityHolder
    {
        public Card Name { get; } = Card.FlagBearer;
        public int Cost { get; } = 4;
        public CardType CardType { get; } = CardType.Action;

        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            return new List<IRule>
            {
                new PlusMoney(2)
            };
        }
        
        public void ResolveOnGainAbilities(IPlayer player)
        {
            player.RuleStack.Push(new GainArtifact(Artifact.Flag));
        }

        public void ResolveOnTrashAbilities(IPlayer player)
        {
            player.RuleStack.Push(new GainArtifact(Artifact.Flag));
        }
    }
}