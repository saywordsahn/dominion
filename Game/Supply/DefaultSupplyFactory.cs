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
                Card.Militia,
                //Card.FlagBearer,
                Card.Beggar,
                Card.Moat,
                Card.Pooka,
                Card.Experiment,
                Card.Cache,
                Card.Witch,
                Card.Marauder,
                Card.LostCity,
                Card.Hideout,
                Card.KingsCourt,
                //Card.Ratcatcher,
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