using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Types
{
    public interface IOnGainAbilityHolder
    {
        void ResolveOnGainAbilities(IPlayer player);
    }
}