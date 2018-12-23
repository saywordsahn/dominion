using DominionWeb.Game.Player;

namespace DominionWeb.Game.Common.Rules
{
    public interface IRule
    {
        void Resolve(Game game, IPlayer player);
        bool Resolved { get; set; }
    }
}