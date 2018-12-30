using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Types
{
    public interface IOnTrashAbilityHolder
    {
        void ResolveOnTrashAbilities(IPlayer player);
    }
}