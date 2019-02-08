using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards;

namespace DominionWeb.Game.Supply
{
    public class DefaultSupplyFactory : ISupplyFactory
    {
        public Supply Create(int numberOfPlayers = 2)
        {
            
            var pileFactory = new PileFactory(numberOfPlayers);

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
                Card.CountingHouse,
                //Card.FlagBearer,
                Card.Groundskeeper,
                Card.Moat,
                Card.Scavenger,
                Card.Expand,
                Card.Armory,
                Card.Embassy,
                Card.Altar,
                Card.Remodel,
                Card.Artisan,
                Card.KingsCourt,
                //Card.Ratcatcher,
                Card.Workshop,
            });
            
            if (kSupply.Any(x => CardLists.Looters.Any(y => y == x.PileCard)))
            {
                //add ruins piles
                var ruinsSupply = pileFactory.Create(Card.VirtualRuins);
                return new Supply(tSupply, vSupply, kSupply, ruinsSupply);
            }
            
            return new Supply(tSupply, vSupply, kSupply);
        }
    }
}