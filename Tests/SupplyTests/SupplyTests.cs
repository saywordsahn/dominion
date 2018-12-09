using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game;
using Xunit;
using DominionWeb.Game.Player;
using DominionWeb.Game.Supply;

namespace DominionWeb.Tests.SupplyTests
{
    public class SupplyTests
    {
        [Fact]
        public void NoProvincesRemain_returns_true()
        {
            var victorySupply = new List<Pile>();
            var kingdomSupply = new List<Pile>();
            var treasureSupply = new List<Pile>();
            
            var supply = new Supply(treasureSupply, victorySupply, kingdomSupply);

            var actual = supply.NoProvincesRemain();
            Assert.True(actual);
        }
        
        [Fact]
        public void NoProvincesRemain_returns_false()
        {
            var victorySupply = new List<Pile>()
            {
                new Pile(new List<Card>()
                {
                    Card.Province
                })
            };
            
            var kingdomSupply = new List<Pile>();
            var treasureSupply = new List<Pile>();
            
            var supply = new Supply(treasureSupply, victorySupply, kingdomSupply);

            var actual = supply.NoProvincesRemain();
            Assert.False(actual);
        }
    }
}