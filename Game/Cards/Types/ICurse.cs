using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Types
{
    public interface ICurse
    {
        int GetVictoryPointValue(IPlayer player);
    }
}