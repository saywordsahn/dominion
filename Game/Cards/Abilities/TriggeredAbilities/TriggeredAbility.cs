using DominionWeb.Game.Player.Triggers;

namespace DominionWeb.Game.Cards.Abilities.TriggeredAbilities
{
    public class TriggeredAbility : ITriggeredAbility
    {
        public ITrigger Trigger { get; }
        public IAbility Ability { get; }
        public TriggeredAbilityDurationType TriggeredAbilityDurationType { get; }
        
        public TriggeredAbility(ITrigger trigger, IAbility ability,
            TriggeredAbilityDurationType durationType)
        {
            Trigger = trigger;
            Ability = ability;
            TriggeredAbilityDurationType = durationType;
        }
    }
}