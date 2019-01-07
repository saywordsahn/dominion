using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Types
{
    public interface ITreasureAbilityHolder
    {
        void ResolveTreasureAbilities(IPlayer player);
    }
}