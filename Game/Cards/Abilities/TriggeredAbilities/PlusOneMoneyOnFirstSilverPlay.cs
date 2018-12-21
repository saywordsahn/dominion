using DominionWeb.Game.Player.Triggers;

namespace DominionWeb.Game.Cards.Abilities.TriggeredAbilities
{
    public class PlusOneMoneyOnFirstSilverPlay : ITriggeredAbility
    {
        public ITrigger Trigger { get; } = new OnPlayTrigger(Card.Silver);
        public IAbility Ability { get; } = new PlusOneMoney();
        public TriggeredAbilityDurationType TriggeredAbilityDurationType { get; } = TriggeredAbilityDurationType.Once;
    }
}