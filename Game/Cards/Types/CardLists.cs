using System.Collections.Generic;

namespace DominionWeb.Game.Cards.Types
{
    public class CardLists
    {
        public static readonly IEnumerable<Card> VictoryCards = new List<Card>
        {
            //BASE
            Card.Estate, Card.Duchy, Card.Province, Card.Gardens,
            
            //INTRIGUE
            Card.Mill, Card.Duke, Card.Harem, Card.Nobles,
            
            //PROSPERITY
            Card.Colony,
            
            //SEASIDE
            Card.Island,
            
            //HINTERLANDS
            Card.SilkRoad,
            Card.Farmland,
            Card.Tunnel,

            //DARK AGES
            Card.Feodum,
            Card.DameJosephine,
            Card.OvergrownEstate,
                
            //ADVENTURES
            Card.DistantLands,
            
            //EMPIRES
            Card.Castles,
            Card.HumbleCastle,
            Card.CrumblingCastle,
            Card.SmallCastle,
            Card.HauntedCastle,
            Card.OpulentCastle,
            Card.SprawlingCastle,
            Card.GrandCastle,
            Card.KingsCastle,
            
            //NOCTURNE
            Card.Cemetery,
            Card.Pasture
        };
    }
}