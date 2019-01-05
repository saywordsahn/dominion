using System.Collections.Generic;
using DominionWeb.Game;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;
using DominionWeb.Game.Supply;
using Xunit;

namespace DominionWeb.Tests.CardTests.DarkAgesCardTests
{
    public class SurvivorsTests
    {
        [Fact]
        public void survivors_reorder_top_2()
        {
            var player = new Player(1, "b");
            var players = new List<IPlayer>() {player};
            var pileFactory = new PileFactory(1);
            var supply = new Supply(
                new List<Pile>(),
                new List<Pile>(),
                new List<Pile>());
            var vc = new VictoryCondition();
            var game = new Game.Game(0, players, supply, vc);


            player.GainToHand(Card.Survivors);
            player.Deck.Add(Card.Copper);
            player.Deck.Add(Card.Silver);

            player.PlayStatus = PlayStatus.ActionPhase;
            player.NumberOfActions++;
            game.Submit(player.PlayerName, PlayerAction.Play, Card.Survivors);
            game.Submit(player.PlayerName, ActionRequestType.SelectOptions, new List<ActionResponse> { ActionResponse.Put });
            game.Submit(player.PlayerName, ActionRequestType.SelectCards, new List<Card> {Card.Silver, Card.Copper});
            game.CheckPlayStack(player);

            var expected = new List<Card> {Card.Silver, Card.Copper};

            Assert.Equal(expected, player.Deck);
        }
    }
}