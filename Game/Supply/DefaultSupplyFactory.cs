using System.Collections.Generic;
using System.Linq;

namespace DominionWeb.Game.Supply
{
    public class DefaultSupplyFactory : ISupplyFactory
    {
        public Supply Create()
        {            
            var coppers = new Pile(Enumerable.Repeat(Card.Copper, 46).ToList());
            var silvers = new Pile(Enumerable.Repeat(Card.Silver, 40).ToList());
            var golds = new Pile(Enumerable.Repeat(Card.Gold, 30).ToList());
            
            var estates = new Pile(Enumerable.Repeat(Card.Estate, 8).ToList());
            var duchys = new Pile(Enumerable.Repeat(Card.Duchy, 8).ToList());
            var provinces = new Pile(Enumerable.Repeat(Card.Province, 8).ToList());
            var curses = new Pile(Enumerable.Repeat(Card.Curse, 10).ToList());
            
            var villages = new Pile(Enumerable.Repeat(Card.Village, 10).ToList());
            var witches = new Pile(Enumerable.Repeat(Card.Witch, 10).ToList());
            var smithys = new Pile(Enumerable.Repeat(Card.Smithy, 10).ToList());
            var markets = new Pile(Enumerable.Repeat(Card.Market, 10).ToList());
            var laboratorys = new Pile(Enumerable.Repeat(Card.Laboratory, 10).ToList());
            var chapels = new Pile(Enumerable.Repeat(Card.Chapel, 10).ToList());
            var moats = new Pile(Enumerable.Repeat(Card.Moat, 10).ToList());
            var gardens = new Pile(Enumerable.Repeat(Card.Gardens, 8).ToList());
            var throneRooms = new Pile(Enumerable.Repeat(Card.ThroneRoom, 10).ToList());
            var vassals = new Pile(Enumerable.Repeat(Card.Vassal, 10).ToList());
            
            var tSupply = new List<Pile>()
            {
                coppers, silvers, golds
            };

            var vSupply = new List<Pile>()
            {
                estates, duchys, provinces, curses
            };

            var kSupply = new List<Pile>()
            {
                villages, witches, smithys, markets, laboratorys,
                chapels, moats, gardens, throneRooms, vassals
            };
            
            return new Supply(tSupply, vSupply, kSupply);
        }
    }
}