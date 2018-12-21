using DominionWeb.Game.Player.Triggers;

namespace DominionWeb.Game.Cards.Abilities.TriggeredAbilities
{
    public interface ITriggeredAbility
    {
        ITrigger Trigger { get; }
        IAbility Ability { get; }
        TriggeredAbilityDurationType TriggeredAbilityDurationType { get; }
    }
}