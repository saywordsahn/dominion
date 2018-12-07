using System.Data.SqlTypes;
using DominionWeb.Game;
using DominionWeb.Game.Player;
using Xunit;

namespace DominionWeb.Tests.PlayerTests
{
    public class PlayerTests
    {
        [Fact]
        public void Player_plays_treasure()
        {
            var player = new Player(1, "ben@gmail.com");
            player.Hand.Add(Card.Copper);
            player.Play(Card.Copper);
            Assert.Equal(1, player.MoneyPlayed);
        }
    }
}