using System;
using System.Collections.Generic;

namespace DominionWeb.Models
{
    public partial class Game
    {
        public Game()
        {
            GameState = new HashSet<GameState>();
        }

        public int GameId { get; set; }
        public DateTime DateTime { get; set; }

        public virtual ICollection<GameState> GameState { get; set; }
    }
}
