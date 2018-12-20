using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;
using DominionWeb.Game.Supply;
using Xunit;

namespace DominionWeb.Tests.CardTests.RenaissanceCardTests
{
    public class DucatTests
    {
        [Fact]
        public void ducat_is_gained_in_action_phase()
        {
            var player = new Player(1, "b");
            var players = new List<IPlayer>() {player};
            var supply = new Supply(
                new List<Pile>(),
                new List<Pile>(), 
                new List<Pile>());
            var vc = new VictoryCondition();
            var game = new Game.Game(0, players, supply, vc);


            player.PlayStatus = PlayStatus.ActionPhase;
            player.GainToHand(Card.Copper);
            player.GainToHand(Card.Ducat);
            game.CheckPlayStack(player);
            game.Submit(player.PlayerName, ActionRequestType.YesNo, ActionResponse.Yes);
            Assert.Equal(PlayStatus.ActionPhase, player.PlayStatus);
            Assert.Single(player.Hand);
            Assert.Single(supply.Trash);
        }
       
    }
}