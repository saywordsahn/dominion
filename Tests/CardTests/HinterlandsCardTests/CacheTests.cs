using System.Collections.Generic;
using DominionWeb.Game;
using DominionWeb.Game.Player;
using DominionWeb.Game.Supply;
using Xunit;

namespace DominionWeb.Tests.CardTests.HinterlandsCardTests
{
    public class CacheTests
    {
        [Fact]
        public void cache_test_gain_during_action_phase()
        {
            var player = new Player(1, "b");
            var players = new List<IPlayer>() {player};
            var supply = new Supply(
                new List<Pile>() { new Pile(Card.Copper, 10) },
                new List<Pile>(), 
                new List<Pile>());
            var vc = new VictoryCondition();
            var game = new Game.Game(0, players, supply, vc);

            player.PlayStatus = PlayStatus.ActionPhase;
            player.Gain(Card.Cache);
            game.CheckPlayStack(player);

            var expected = new List<Card> {Card.Cache, Card.Copper, Card.Copper};
            Assert.Equal(expected, player.DiscardPile);
        }
    }
}