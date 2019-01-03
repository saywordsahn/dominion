using System.Collections.Generic;
using DominionWeb.Game;
using DominionWeb.Game.Supply;
using Xunit;

namespace DominionWeb.Tests.SupplyTests
{
    public class SplitPileTests
    {
        [Fact]
        public void split_pile_tests()
        {
            var splitPile = new SplitPile(Card.PatricianEmporium, Card.Patrician, Card.Emporium);

            var expected = new List<Card>
            {
                Card.Emporium,
                Card.Emporium,
                Card.Emporium,
                Card.Emporium,
                Card.Emporium,
                Card.Patrician,
                Card.Patrician,
                Card.Patrician,
                Card.Patrician,
                Card.Patrician
            };
            
            Assert.Equal(expected, splitPile.Cards);
        }
    }
}