using DominionWeb.Game.Common.Rules;

namespace DominionWeb.Game.Cards
{
    public interface IAttackReaction
    {
        IRule ReactionEffect();
    }
}