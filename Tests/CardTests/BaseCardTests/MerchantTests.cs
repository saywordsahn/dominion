using System.Collections.Generic;
using DominionWeb.Game;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;
using DominionWeb.Game.Supply;
using Xunit;

namespace DominionWeb.Tests.CardTests.BaseCardTests
{
    public class MerchantTests
    {
        [Fact]
        public void merchant_tests()
        {
            var playerA = new Player(1, "a");
            var players = new List<IPlayer>() {playerA};
            var supply = new Supply(
                new List<Pile>(),
                new List<Pile>(),
                new List<Pile>());
            var vc = new VictoryCondition();
            var game = new Game.Game(0, players, supply, vc);

            playerA.GainToHand(Card.Merchant);
            playerA.GainToHand(Card.Silver);
            
            playerA.PlayStatus = PlayStatus.ActionPhase;
            game.Submit(playerA.PlayerName, PlayerAction.Play, Card.Merchant);
            playerA.PlayStatus = PlayStatus.BuyPhase;
            game.Submit(playerA.PlayerName, PlayerAction.Play, Card.Silver);

            Assert.Equal(3, playerA.MoneyPlayed);
        }
    }
}