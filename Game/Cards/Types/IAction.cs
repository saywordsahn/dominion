using System;
using System.Collections.Generic;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Types
{
    public interface IAction : IRulesHolder
    {
        [Obsolete("Resolve is deprecated, please use IRulesHolder interface instead.")]
        void Resolve(Game game);
        IEnumerable<IRule> GetRules(Game game, IPlayer player);
    }
}