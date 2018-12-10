using System.Data.SqlTypes;
using System.Linq;
using DominionWeb.Game;
using DominionWeb.Game.Cards.Base;
using DominionWeb.Game.Player;
using Xunit;

namespace DominionWeb.Tests.PlayerTests
{
    public class PlayerTests
    {
        [Fact]
        public void play_treasure_during_buy_phase_money_increases()
        {
            var player = new Player(1, "ben@gmail.com");
            player.PlayStatus = PlayStatus.BuyPhase;
            player.Hand.Add(Card.Copper);
            player.Play(Card.Copper);
            Assert.Equal(1, player.MoneyPlayed);
        }
        
        [Theory]
        [InlineData(0,0,0,0,0)]
        [InlineData(10,5,2,4,45)]
        [InlineData(10,10,0,20, 120)]
        public void get_victory_point_count(int estates, int duchys, int provinces, int gardens, int expected)
        {
            var player = new Player(1, "ben@gmail.com");

            var cards = Enumerable.Repeat(Card.Estate, estates)
                .Concat(Enumerable.Repeat(Card.Duchy, duchys))
                .Concat(Enumerable.Repeat(Card.Province, provinces))
                .Concat(Enumerable.Repeat(Card.Gardens, gardens));
            
            player.Deck.AddRange(cards);

            var actual = player.GetVictoryPointCount();
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(0,0,0,0)]
        [InlineData(13,5,2,20)]
        [InlineData(10,20,0,30)]
        public void dominion_count(int deck, int discard, int hand, int expected)
        {
            var player = new Player(1, "ben@gmail.com");

            player.Deck.AddRange(Enumerable.Repeat(Card.Copper, deck));
            player.DiscardPile.AddRange(Enumerable.Repeat(Card.Copper, discard));
            player.Hand.AddRange(Enumerable.Repeat(Card.Copper, hand));
           
            var actual = player.DominionCount;
            Assert.Equal(expected, actual);
        }
        
        
        
    }
}