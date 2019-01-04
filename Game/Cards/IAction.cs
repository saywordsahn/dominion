using System;

namespace DominionWeb.Game.Cards
{
    public interface IAction
    {
        [Obsolete("Resolve is deprecated, please use IRulesHolder interface instead.")]
        void Resolve(Game game);
    }
}