using System.Collections.Generic;
using DominionWeb.Game;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;
using DominionWeb.Game.Supply;
using Xunit;

namespace DominionWeb.Tests.CardTests.EmpiresCardsTests
{
    public class EmporiumTests
    {
        [Fact]
        public void emporium_gained_with_5_actions_in_play()
        {
            var player = new Player(1, "b");
            var players = new List<IPlayer>() {player};
            var pileFactory = new PileFactory(1);
            var supply = new Supply(
                new List<Pile>(),
                new List<Pile>(), 
                pileFactory.Create(new List<Card> { Card.Emporium }));
            var vc = new VictoryCondition();
            var game = new Game.Game(0, players, supply, vc);
            
            
            player.GainToHand(Card.Village);
            player.GainToHand(Card.Village);
            player.GainToHand(Card.Village);
            player.GainToHand(Card.Village);
            player.GainToHand(Card.Village);
            player.PlayStatus = PlayStatus.ActionPhase;
            game.Submit(player.PlayerName, PlayerAction.Play, Card.Village);
            game.Submit(player.PlayerName, PlayerAction.Play, Card.Village);
            game.Submit(player.PlayerName, PlayerAction.Play, Card.Village);
            game.Submit(player.PlayerName, PlayerAction.Play, Card.Village);
            game.Submit(player.PlayerName, PlayerAction.Play, Card.Village);
            player.EndActionPhase();
            player.MoneyPlayed = int.MaxValue;
            player.NumberOfBuys = int.MaxValue;
            game.Submit(player.PlayerName, PlayerAction.Buy, Card.Emporium);
            
            Assert.Equal(2, player.VictoryTokens);
        }
        
        
        [Fact]
        public void emporium_gained_from_remodel()
        {
            var player = new Player(1, "b");
            var players = new List<IPlayer>() {player};
            var pileFactory = new PileFactory(1);
            var supply = new Supply(
                new List<Pile>(),
                new List<Pile>(), 
                pileFactory.Create(new List<Card> { Card.Emporium }));
            var vc = new VictoryCondition();
            var game = new Game.Game(0, players, supply, vc);
            
            
            player.GainToHand(Card.Village);
            player.GainToHand(Card.Village);
            player.GainToHand(Card.Village);
            player.GainToHand(Card.Village);
            player.GainToHand(Card.Gold);
            player.GainToHand(Card.Remodel);
            player.PlayStatus = PlayStatus.ActionPhase;
            game.Submit(player.PlayerName, PlayerAction.Play, Card.Village);
            game.Submit(player.PlayerName, PlayerAction.Play, Card.Village);
            game.Submit(player.PlayerName, PlayerAction.Play, Card.Village);
            game.Submit(player.PlayerName, PlayerAction.Play, Card.Village);
            game.Submit(player.PlayerName, PlayerAction.Play, Card.Remodel);
            game.Submit(player.PlayerName, ActionRequestType.SelectCards, new List<Card> { Card.Gold });
            game.Submit(player.PlayerName, ActionRequestType.SelectCards, new List<Card> { Card.Emporium });
            
            Assert.Equal(2, player.VictoryTokens);
        }
    }
}