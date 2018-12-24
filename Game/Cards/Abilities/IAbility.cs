using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public interface IAbility : IRule
    {
        void Resolve(Game game, IPlayer player);
        bool Resolved { get; set; }
    }
}