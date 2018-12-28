using System.Collections.Generic;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards
{
    /// <summary>
    /// A temporary class to allow cards to be played as rules during the action phase.
    /// </summary>
    public interface IRulesHolder
    {
        IEnumerable<IRule> GetRules(Game game, IPlayer player);
    }
}