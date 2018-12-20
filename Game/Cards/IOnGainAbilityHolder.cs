using DominionWeb.Game.Cards.Abilities;

namespace DominionWeb.Game.Cards
{
    public interface IOnGainAbilityHolder
    {
        IAbility OnGainAbility { get; set; }
    }
}