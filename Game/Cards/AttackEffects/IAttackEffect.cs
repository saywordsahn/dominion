using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.AttackEffects
{
    //TODO: rework to get rid of IRule and replace with other
    public interface IAttackEffect : IRule
    {
        bool Resolved { get; set; }

        void Resolve(Game game, IPlayer player);

    }
}