using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class PlusOneMoneyOnFirstSilverPlay : ITriggeredAbility
    {
        public ITrigger Trigger { get; } = new OnPlayTrigger(Card.Silver);
        public IAbility Ability { get; } = new PlusOneMoney();
        public TriggeredAbilityDurationType TriggeredAbilityDurationType { get; } = TriggeredAbilityDurationType.Once;
    }
}