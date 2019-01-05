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
                Card.FlagBearer,
                Card.SilkMerchant,
                Card.Experiment,
                Card.Cache,
                Card.Witch,
                Card.Ranger,
                Card.LostCity,
                Card.Hideout,
                Card.KingsCourt,
                Card.Ratcatcher,
                Card.Miser,
            });

            var workingSupply = pileFactory.Create(new List<Card>
            {
                Card.Miser,
                Card.Wharf,
                Card.Harem,
                Card.Island,
                Card.SilkRoad,
                Card.Monument,
                Card.KingsCourt,
                Card.GrandMarket,
                Card.JunkDealer,
                Card.ActingTroupe
            });
            
            return new Supply(tSupply, vSupply, workingSupply);
        }
    }
}