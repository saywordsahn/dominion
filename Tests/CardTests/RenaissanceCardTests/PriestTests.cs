using System.Collections.Generic;
using DominionWeb.Game;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;
using DominionWeb.Game.Supply;
using Xunit;

namespace DominionWeb.Tests.CardTests.RenaissanceCardTests
{
    public class PriestTests
    {
        [Fact]
        public void priest_is_played_with_trashing()
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
            player.GainToHand(Card.Copper);
            player.GainToHand(Card.Copper);
            player.GainToHand(Card.Priest);
            player.GainToHand(Card.Chapel);
            player.NumberOfActions = 2;
            game.Submit(player.PlayerName, PlayerAction.Play, Card.Priest);
            game.Submit(player.PlayerName, ActionRequestType.SelectCards, new List<Card> { Card.Copper });
            game.Submit(player.PlayerName, PlayerAction.Play, Card.Chapel);
            game.Submit(player.PlayerName, ActionRequestType.SelectCards, new List<Card> { Card.Copper, Card.Copper });

            Assert.Equal(6, player.MoneyPlayed);
        }
    }
}