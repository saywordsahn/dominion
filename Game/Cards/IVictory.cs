using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards
{
    public interface IVictory
    {
        int GetVictoryPointValue(IPlayer player);
    }
}