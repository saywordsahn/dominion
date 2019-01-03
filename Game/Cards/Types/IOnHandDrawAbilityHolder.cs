using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Types
{
    public interface IOnHandDrawAbilityHolder
    {
        void OnHandDraw(IPlayer player);
    }
}