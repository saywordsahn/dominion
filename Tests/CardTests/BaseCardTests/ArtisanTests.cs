using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game;
using DominionWeb.Game.Common;
using Xunit;
using DominionWeb.Game.Player;
using DominionWeb.Game.Supply;

namespace DominionWeb.Tests.CardTests.BaseCardTests
{
    public class SupplyTests
    {
        [Theory]
        [InlineData(Card.Copper)]
        [InlineData(Card.Duchy)]
        public void artisan_card_is_gained(Card card)
        {
            const int supplyAmount = 10;
            var player = new Player(1, "b");
            var players = new List<IPlayer>() {player};
            var supply = new Supply(new List<Pile>(), new List<Pile>(), 
                new List<Pile>()
                {
                    new Pile(card, supplyAmount)
                });
            var vc = new VictoryCondition();
            var game = new Game.Game(0, players, supply, vc);
            
            player.GainToHand(Card.Artisan);
            player.PlayStatus = PlayStatus.ActionPhase;
            game.Submit(player.PlayerName, PlayerAction.Play, Card.Artisan);
            game.Submit(player.PlayerName, ActionRequestType.SelectCards, new List<Card> { card });

            var actualSupplyCount = game.Supply.KingdomSupply.First().Cards.Count;
            
            Assert.Single(player.DiscardPile, x => x == card);
            Assert.Equal(actualSupplyCount, supplyAmount - 1);
        }
        
        [Fact]
        public void artisan_card_too_expensive()
        {
            const Card card = Card.Artisan;
            const int supplyAmount = 10;
            
            var player = new Player(1, "b");
            var players = new List<IPlayer>() {player};
            var supply = new Supply(new List<Pile>(), new List<Pile>(), 
                new List<Pile>()
                {
                    new Pile(card, supplyAmount),
                    new Pile(Card.Chapel, 10)
                });
            var vc = new VictoryCondition();
            var game = new Game.Game(0, players, supply, vc);
            
            player.GainToHand(Card.Artisan);
            player.PlayStatus = PlayStatus.ActionPhase;
            game.Submit(player.PlayerName, PlayerAction.Play, Card.Artisan);
            game.Submit(player.PlayerName, ActionRequestType.SelectCards, new List<Card> { card });

            var actualSupplyCount = game.Supply.KingdomSupply.First().Cards.Count;
            
            Assert.Empty(player.DiscardPile);
            Assert.Equal(actualSupplyCount, supplyAmount);
        }
        
    }
}