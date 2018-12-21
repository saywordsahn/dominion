using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards
{
    public interface ITreasureAbilityHolder
    {
        void ResolveTreasureAbilities(IPlayer player);
    }
}