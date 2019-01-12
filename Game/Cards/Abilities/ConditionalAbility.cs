using DominionWeb.Game.Cards.Abilities.Conditions;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class ConditionalAbility : IAbility
    {
        public IAbility ConditionMetAbility { get; set; }
        public IAbility ConditionNotMetAbility { get; set; }
        public IAbilityCondition Condition { get; set; }

        public ConditionalAbility(IAbilityCondition condition, IAbility conditionMetAbility, IAbility conditionNotMetAbility = null)
        {
            Condition = condition;
            ConditionMetAbility = conditionMetAbility;
            ConditionNotMetAbility = conditionNotMetAbility;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            if (Condition.IsMet(game, player))
            {
                player.RuleStack.Push(ConditionMetAbility);
            }
            else if (ConditionNotMetAbility != null)
            {
                player.RuleStack.Push(ConditionNotMetAbility);
            }
            
            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}