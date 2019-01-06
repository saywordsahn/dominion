using System.Collections.Generic;

namespace DominionWeb.Game.Cards
{
    public static class CardLists
    {
        public static Dictionary<Card, Card> CardPileMap = new Dictionary<Card, Card>
        {
            { Card.Patrician, Card.PatricianEmporium },
            { Card.Emporium, Card.PatricianEmporium },
            { Card.Encampment, Card.EncampmentPlunder },
            { Card.Plunder, Card.EncampmentPlunder },
            { Card.Settlers, Card.SettlersBustlingVillage },
            { Card.BustlingVillage, Card.SettlersBustlingVillage },
            { Card.Catapult, Card.CatapultRocks },
            { Card.Rocks, Card.CatapultRocks },
            { Card.Gladiator, Card.GladiatorFortune },
            { Card.Fortune, Card.GladiatorFortune }, 
            { Card.KingsCourt, Card.Castles },
            { Card.GrandCastle, Card.Castles },
            { Card.SprawlingCastle, Card.Castles },
            { Card.OpulentCastle, Card.Castles },
            { Card.HauntedCastle, Card.Castles },
            { Card.SmallCastle, Card.Castles },
            { Card.CrumblingCastle, Card.Castles },
            { Card.HumbleCastle, Card.Castles },
            { Card.Sauna, Card.SaunaAvanto },
            { Card.Avanto, Card.SaunaAvanto }
        };

        public static List<Card> Looters = new List<Card>
        {
            Card.DeathCart,
            Card.Marauder,
            Card.Cultist
        };

    }
}