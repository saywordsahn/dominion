using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;
using DominionWeb.Game.Supply;
using Xunit;

namespace DominionWeb.Tests.CardTests.BaseCardTests
{
    public class CellarTests
    {
//        [Theory]
//        [InlineData(Card.Copper)]
//        [InlineData(Card.Duchy)]
//        public void cellar_card_is_gained(Card card)
//        {
//            const int supplyAmount = 10;
//            var player = new Player(1, "b");
//            var players = new List<IPlayer>() {player};
//            var supply = new Supply(new List<Pile>(), new List<Pile>(), 
//                new List<Pile>()
//                {
//                    new Pile(card, supplyAmount)
//                });
//            var vc = new VictoryCondition();
//            var game = new Game.Game(0, players, supply, vc);
//            
//            player.GainToHand(Card.Artisan);
//            player.PlayStatus = PlayStatus.ActionPhase;
//            game.Submit(player.PlayerName, PlayerAction.Play, Card.Artisan);
//            game.Submit(player.PlayerName, ActionRequestType.SelectCards, new List<Card> { card });
//
//            var actualSupplyCount = game.Supply.KingdomSupply.First().Cards.Count;
//            
//            Assert.Single(player.DiscardPile, x => x == card);
//            Assert.Equal(actualSupplyCount, supplyAmount - 1);
//        }
        
        [Fact]
        public void cellar_discard_success()
        {
            var player = new Player(1, "b");
            var players = new List<IPlayer>() {player};
            var supply = new Supply(new List<Pile>(), new List<Pile>(), new List<Pile>());
            var vc = new VictoryCondition();
            var game = new Game.Game(0, players, supply, vc);
            
            player.GainToHand(Card.Cellar);
            player.Hand.AddRange(Enumerable.Repeat(Card.Copper, 4));
            player.Deck.AddRange(Enumerable.Repeat(Card.Silver, 5));
            player.PlayStatus = PlayStatus.ActionPhase;
            game.Submit(player.PlayerName, PlayerAction.Play, Card.Cellar);
            game.Submit(player.PlayerName, ActionRequestType.SelectCards, Enumerable.Repeat(Card.Copper, 3));

            var coppersInHand = player.Hand.Count(x => x == Card.Copper);
            var silversInHand = player.Hand.Count(x => x == Card.Silver);
            var discardCount = player.DiscardPile.Count();
            
            Assert.Equal(1, coppersInHand);
            Assert.Equal(3, silversInHand);
            Assert.Equal(3, discardCount);
            Assert.Equal(1, player.PlayedCards.Count);
            Assert.Equal(2, player.Deck.Count);
        }
    }
}