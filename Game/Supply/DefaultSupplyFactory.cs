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
                Card.Cache,
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
                Card.Sacrifice,
                Card.Plunder,
                Card.Merchant,
                Card.Remodel,
                Card.Lighthouse,
                Card.Witch,
                Card.ThroneRoom,
                Card.NomadCamp,
                Card.Market,
                Card.BanditCamp
            });
            
            return new Supply(tSupply, vSupply, workingSupply);
        }
    }
}