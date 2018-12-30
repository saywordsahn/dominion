using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards
{
    public interface IOnGainAbilityHolder
    {
        IAbility OnGainAbility { get; set; }
        void ResolveOnGainAbilities(IPlayer player);
    }
}