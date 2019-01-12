using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.Conditions
{
    public interface IAbilityCondition
    {
        bool IsMet(Game game, IPlayer player);
    }
}