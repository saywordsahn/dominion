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
                Card.Scholar,
                Card.FlagBearer,
                Card.SilkMerchant,
                Card.Experiment,
                Card.Cache,
                Card.Witch,
                Card.Island,
                Card.Spices,
                Card.Hideout,
                Card.ThroneRoom,
                Card.GrandMarket
                
            });

            var workingSupply = pileFactory.Create(new List<Card>
            {
                Card.Lurker,
                Card.Plunder,
                Card.PatricianEmporium,
                Card.Island,
                Card.Monument,
                Card.Witch,
                Card.ThroneRoom,
                Card.NomadCamp,
                Card.Ducat,
                Card.BanditCamp
            });
            
            return new Supply(tSupply, vSupply, kSupply);
        }
    }
}