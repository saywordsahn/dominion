namespace DominionWeb.Models
{
    public partial class GameState
    {
        public int GameStateId { get; set; }
        public int GameId { get; set; }
        public string State { get; set; }

        public Game Game { get; set; }
    }
}
