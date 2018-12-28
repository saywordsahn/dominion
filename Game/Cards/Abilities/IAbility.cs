using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public interface IAbility : IRule
    {
        bool Resolved { get; set; }
        void Resolve(Game game, IPlayer player);
    }
}