using System.Collections.Generic;
using DominionWeb.Game;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;
using DominionWeb.Game.Supply;
using Xunit;

namespace DominionWeb.Tests.CardTests.HinterlandsCardTests
{
    public class NomadCampTests
    {
        [Fact]
        public void nomad_camp_gained_during_buy_phase()
        {
            var player = new Player(1, "b");
            var players = new List<IPlayer>() {player};
            var supply = new Supply(
                new List<Pile>(),
                new List<Pile>(), 
                new List<Pile>() { new Pile(Card.NomadCamp, 10) });
            var vc = new VictoryCondition();
            var game = new Game.Game(0, players, supply, vc);

            player.PlayStatus = PlayStatus.BuyPhase;
            player.NumberOfBuys = 1;
            player.MoneyPlayed = 10;
            game.Submit(player.PlayerName, PlayerAction.Buy, Card.NomadCamp);
            Assert.Single(player.Deck);
            Assert.Empty(player.DiscardPile);
        }
    }
}