using System;
using DominionWeb.Game.Cards;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Common.Rules
{
    public class Rule : IRule
    {
        public bool Resolved { get; set; }
        public Action<Game, IPlayer> Action { get; set; }

        public Rule(Action<Game, IPlayer> action)
        {
            Action = action;
        }
            
        //note: this can only be used with simple actions that don't require responses
        public void Resolve(Game game, IPlayer player)
        {
            Action(game, player);
            Resolved = true;
        }

    }
}