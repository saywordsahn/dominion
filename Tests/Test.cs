using DominionWeb.Game.Player;
using Xunit;

namespace DominionWeb.Tests
{
    public class Test
    {
        [Fact]
        public void FirstTest()
        {
            var player = new Game.Player.Player(1, "ben@gmail.com");
        }
    }
}