using DominionWeb.Game.Common.Rules;

namespace DominionWeb.Game.Cards.Types
{
    public interface IAttackReaction
    {
        IRule ReactionEffect();
    }
}