using System.Collections.Generic;
using System.Linq;

namespace DominionWeb.Game.Supply
{
    public class DefaultSupplyFactory : ISupplyFactory
    {
        public Supply Create()
        {
            
            var pileFactory = new PileFactory(2);

            var tSupply = pileFactory.Create(new List<Card>
                {
                    Card.Copper, Card.Silver, Card.Gold
                });

            var vSupply = pileFactory.Create(new List<Card>
            {
                Card.Estate, Card.Duchy, Card.Province, Card.Curse
            });

            var kSupply = pileFactory.Create(new List<Card>
            {
                Card.Ducat,
                Card.Moat,
                Card.Village,
                Card.Remodel,
                Card.Poacher,
                Card.Witch,
                Card.Militia,
                Card.Bandit,
                Card.Artisan,
                Card.ThroneRoom,
                Card.Beggar,
                Card.Lighthouse
            });

            var workingSupply = pileFactory.Create(new List<Card>
            {
                Card.Chapel,
                Card.Witch,
                Card.Beggar,
                Card.Remodel,
                Card.Lighthouse,
                Card.Vassal,
                Card.ThroneRoom,
                Card.NomadCamp,
                Card.Market,
                Card.Laboratory
            });
            
            return new Supply(tSupply, vSupply, kSupply);
        }
    }
}