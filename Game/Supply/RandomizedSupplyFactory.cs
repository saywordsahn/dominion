using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards;
using DominionWeb.Game.Utils;

namespace DominionWeb.Game.Supply
{
    public class RandomizedSupplyFactory : ISupplyFactory
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

            var availableCards = CardLists.AvailableCards.ToList();
            
            availableCards.Shuffle();

            var topTen = availableCards.Take(10);

            var kSupply = pileFactory.Create(topTen);
            
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