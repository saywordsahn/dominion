using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Types
{
    public interface IOnBuyAbilityHolder
    {
        void ResolveOnGainAbilities(IPlayer player);
    }
}