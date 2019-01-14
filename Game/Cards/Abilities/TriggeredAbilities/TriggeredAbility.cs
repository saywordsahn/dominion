using DominionWeb.Game.Player.Triggers;

namespace DominionWeb.Game.Cards.Abilities.TriggeredAbilities
{
    public class TriggeredAbility : ITriggeredAbility
    {
        public ITrigger Trigger { get; set; }
        public IAbility Ability { get; set; }
        public TriggeredAbilityDurationType TriggeredAbilityDurationType { get; set; }
        
        public TriggeredAbility(ITrigger trigger, IAbility ability,
            TriggeredAbilityDurationType durationType)
        {
            Trigger = trigger;
            Ability = ability;
            TriggeredAbilityDurationType = durationType;
        }
    }
}