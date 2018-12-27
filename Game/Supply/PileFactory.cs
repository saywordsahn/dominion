using System;
using System.Collections.Generic;
using System.Linq;

namespace DominionWeb.Game.Supply
{
    public class PileFactory
    {
        private readonly int _numberOfPlayers;

        public PileFactory(int numberOfPlayers)
        {
            _numberOfPlayers = numberOfPlayers;
        }

        
        //TODO: work victory card list into the builder
        private Pile Create(Card card)
        {
            
            var victorySupplyAmount = _numberOfPlayers == 2 ? 8 : 12;
            
            switch (card)
            {
                //BASE
                case Card.Copper:
                    return new Pile(Card.Copper, 60 - 7 * _numberOfPlayers);
                case Card.Silver:
                    return new Pile(Card.Silver, 40);
                case Card.Gold:
                    return new Pile(Card.Gold, 30);
                case Card.Estate:                    
                case Card.Duchy:
                case Card.Province:
                case Card.Gardens:
                    return new Pile(card, victorySupplyAmount);
                case Card.Curse:
                    return new Pile(Card.Curse, (_numberOfPlayers - 1) * 10);
                
                
                //INTRIGUE
                case Card.Mill:
                case Card.Duke:
                case Card.Harem:
                case Card.Nobles:
                    return new Pile(card, victorySupplyAmount);
                
                //PROSPERITY
                case Card.Platinum:
                    return new Pile(Card.Gold, 12);
                case Card.Colony:
                    return new Pile(card, victorySupplyAmount);
                    
                
                //SEASIDE
                case Card.Island:
                    return new Pile(card, victorySupplyAmount);
                
                //HINTERLANDS
                case Card.SilkRoad:
                case Card.Farmland:
                    return new Pile(card, victorySupplyAmount);

                //DARK AGES
                case Card.Feodum:
                    return new Pile(card, victorySupplyAmount);
                
                //ADVENTURES
                case Card.DistantLands:
                    return new Pile(card, victorySupplyAmount);
                
                //EMPIRES
                case Card.PatricianEmporium:
                    return new SplitPile(Card.Patrician, Card.Emporium);
                case Card.EncampmentPlunder:
                    return new SplitPile(Card.Encampment, Card.Plunder);
                case Card.SettlersBustlingVillage:
                    return new SplitPile(Card.Settlers, Card.BustlingVillage);
                case Card.CatapultRocks:
                    return new SplitPile(Card.Catapult, Card.Rocks);
                case Card.GladiatorFortune:
                    return new SplitPile(Card.Gladiator, Card.Fortune);
                case Card.Castles:
                    if (_numberOfPlayers > 2)
                    {
                        return new Pile(new List<Card>
                        {
                            Card.KingsCastle,
                            Card.GrandCastle,
                            Card.SprawlingCastle,
                            Card.OpulentCastle,
                            Card.HauntedCastle,
                            Card.SmallCastle,
                            Card.CrumblingCastle,
                            Card.HumbleCastle
                        });
                    }
                    else
                    {
                        return new Pile(new List<Card>
                        {
                            Card.KingsCastle,
                            Card.KingsCastle,
                            Card.GrandCastle,
                            Card.SprawlingCastle,
                            Card.OpulentCastle,
                            Card.OpulentCastle,
                            Card.HauntedCastle,
                            Card.SmallCastle,
                            Card.SmallCastle,
                            Card.CrumblingCastle,
                            Card.HumbleCastle,
                            Card.HumbleCastle
                        });
                    }
                        
                //PROMO
                case Card.SaunaAvanto:
                    return new SplitPile(Card.Sauna, Card.Avanto);
                
                
                default:
                    return new Pile(card, 10);
            }
            
        }
        

        public IEnumerable<Pile> Create(IEnumerable<Card> cards)
        {
            return cards.Select(Create).ToList();
        }
    }
}