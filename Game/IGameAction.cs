using DominionWeb.Game.Player;

namespace DominionWeb.Game
{
    public interface IGameAction
    {
        bool Resolved { get; set; }
        void Resolve(Game game, IPlayer player);
    }
}