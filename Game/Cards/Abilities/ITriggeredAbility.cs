using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public interface ITriggeredAbility
    {
        ITrigger Trigger { get; }
        IAbility Ability { get; }
        TriggeredAbilityDurationType TriggeredAbilityDurationType { get; }
    }
}