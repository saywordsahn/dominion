using System.Collections.Generic;
using System.Linq;

namespace DominionWeb.Game.Supply
{
    public class DefaultSupplyFactory : ISupplyFactory
    {
        public Supply Create()
        {            
            var coppers = new Pile(Card.Copper, 46);
            var silvers = new Pile(Card.Silver, 40);
            var golds = new Pile(Card.Gold, 30);
            
            var estates = new Pile(Card.Estate, 8);
            var duchys = new Pile(Card.Duchy, 8);
            var provinces = new Pile(Card.Province, 8);
            var curses = new Pile(Card.Curse, 10);
            
            var villages = new Pile(Card.Village, 10);
            var witches = new Pile(Card.Witch, 10);
            var smithys = new Pile(Card.Smithy, 10);
            var markets = new Pile(Card.Market, 10);
            var laboratorys = new Pile(Card.Laboratory, 10);
            var chapels = new Pile(Card.Chapel, 10);
            var moats = new Pile(Card.Moat, 10);
            var gardens = new Pile(Card.Gardens, 8);
            var throneRooms = new Pile(Card.ThroneRoom, 10);
            var vassals = new Pile(Card.Vassal, 10);
            var councilRooms = new Pile(Card.CouncilRoom, 10);
            var harbingers = new Pile(Card.Harbinger, 10);
            var merchants = new Pile(Card.Merchant, 10);
            var workshops = new Pile(Card.Workshop, 10);
            
            var bureaucrats = new Pile(Card.Bureaucrat, 10);
            var militias = new Pile(Card.Militia, 10);
            var moneyLenders = new Pile(Card.Moneylender, 10);
            var poachers = new Pile(Card.Poacher, 10);
            var remodels = new Pile(Card.Remodel, 10);
            var cellars = new Pile(Card.Cellar, 10);
            
            var sentrys = new Pile(Card.Sentry, 10);
            var mines = new Pile(Card.Mine, 10);
            var librarys = new Pile(Card.Library, 10);
            var bandits = new Pile(Card.Bandit, 10);
            var artisans = new Pile(Card.Artisan, 10);
            
            var nomadCamps = new Pile(Card.NomadCamp, 10);
            var ducats = new Pile(Card.Ducat, 10);
            
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
                ducats, moats, villages, remodels, poachers, mines, 
                militias, bandits, artisans, throneRooms, bureaucrats, nomadCamps           
            };
            
            return new Supply(tSupply, vSupply, kSupply);
        }
    }
}