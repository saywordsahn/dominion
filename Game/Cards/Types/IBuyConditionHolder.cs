using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Types
{
    public interface IBuyConditionHolder
    {
        bool ResolveBuyCondition(Game game, IPlayer player);
    }
}