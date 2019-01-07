using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Types
{
    public interface IOnGainOverride
    {
        void OnGain(IPlayer player, Card card);
    }
}