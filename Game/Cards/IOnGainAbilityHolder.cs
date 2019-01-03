using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards
{
    public interface IOnGainAbilityHolder
    {
        void ResolveOnGainAbilities(IPlayer player);
    }
}