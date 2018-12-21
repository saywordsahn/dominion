using DominionWeb.Game.Player.Triggers;

namespace DominionWeb.Game.Cards.Abilities.TriggeredAbilities
{
    public class PlusTwoMoneyOnTrash : ITriggeredAbility
    {
        public ITrigger Trigger { get; } = new OnTrashTrigger(Card.Any);
        public IAbility Ability => new PlusTwoMoney();
        public TriggeredAbilityDurationType TriggeredAbilityDurationType { get; } = TriggeredAbilityDurationType.Turn;
    }
}