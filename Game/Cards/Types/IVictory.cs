using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Types
{
    public interface IVictory
    {
        int GetVictoryPointValue(IPlayer player);
    }
}