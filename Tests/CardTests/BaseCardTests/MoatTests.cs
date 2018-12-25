using System.Collections.Generic;
using DominionWeb.Game;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;
using DominionWeb.Game.Supply;
using Xunit;

namespace DominionWeb.Tests.CardTests.BaseCardTests
{
    public class MoatTests
    {
        [Fact]
        public void moat_played_against_witch()
        {
            var playerA = new Player(1, "a");
            var playerB = new Player(1, "b");
            var players = new List<IPlayer>() {playerA, playerB};
            var supply = new Supply(
                new List<Pile> { new Pile(Card.Curse, 10)},
                new List<Pile>(), 
                new List<Pile>());
            var vc = new VictoryCondition();
            var game = new Game.Game(0, players, supply, vc);

            playerA.GainToHand(Card.Witch);
            playerB.GainToHand(Card.Moat);
            playerA.PlayStatus = PlayStatus.ActionPhase;
            game.Submit(playerA.PlayerName, PlayerAction.Play, Card.Witch);
            game.Submit(playerB.PlayerName, ActionRequestType.SelectCards, new List<Card> { Card.Moat });
            
            Assert.Empty(playerB.DiscardPile);
        }
    }
}