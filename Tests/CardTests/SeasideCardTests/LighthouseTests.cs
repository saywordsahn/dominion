using System.Collections.Generic;
using DominionWeb.Game;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;
using DominionWeb.Game.Supply;
using Xunit;

namespace DominionWeb.Tests.CardTests.SeasideCardTests
{
    public class LighthouseTests
    {
        [Fact]
        public void lighthouse_is_played()
        {
            var player = new Player(1, "b");
            var players = new List<IPlayer>() {player};
            var supply = new Supply(
                new List<Pile>(),
                new List<Pile>(), 
                new List<Pile>());
            var vc = new VictoryCondition();
            var game = new Game.Game(0, players, supply, vc);

            player.GainToHand(Card.Lighthouse);
            player.PlayStatus = PlayStatus.ActionPhase;
            game.Submit(player.PlayerName, PlayerAction.Play, Card.Lighthouse);
            Assert.Equal(1, player.MoneyPlayed);
            player.EndTurn();
            player.StartTurn(game);
            game.CheckPlayStack(player);
            
            Assert.Equal(1, player.MoneyPlayed);
        }
    }
    
}