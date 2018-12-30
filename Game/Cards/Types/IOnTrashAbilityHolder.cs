using DominionWeb.Game.Cards.Abilities;

namespace DominionWeb.Game.Cards.Types
{
    public interface IOnTrashAbilityHolder
    {
        IAbility OnTrashAbility { get; set; }
    }
}