using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Utils;

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
        public IPile Create(Card card)
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
                case Card.VirtualRuins:
                    var list = Enumerable.Repeat(Card.AbandonedMine, 10)
                        .Concat(Enumerable.Repeat(Card.RuinedLibrary, 10)
                        .Concat(Enumerable.Repeat(Card.RuinedMarket, 10)
                        .Concat(Enumerable.Repeat(Card.RuinedVillage, 10)
                        .Concat(Enumerable.Repeat(Card.Survivors, 10))))).ToList();
                    list.Shuffle();
                    var numberToInclude = (_numberOfPlayers - 1) * 10;
                    list.RemoveRange(numberToInclude - 1, list.Count - numberToInclude);
                    return new Pile(Card.VirtualRuins, list);;
                case Card.Feodum:
                    return new Pile(card, victorySupplyAmount);
                
                //ADVENTURES
                case Card.DistantLands:
                    return new Pile(card, victorySupplyAmount);
                case Card.Port:
                    return new Pile(card, 12);
                
                //EMPIRES
                case Card.PatricianEmporium:
                    return new SplitPile(Card.PatricianEmporium, Card.Patrician, Card.Emporium);
                case Card.EncampmentPlunder:
                    return new SplitPile(Card.EncampmentPlunder,Card.Encampment, Card.Plunder);
                case Card.SettlersBustlingVillage:
                    return new SplitPile(Card.SettlersBustlingVillage,Card.Settlers, Card.BustlingVillage);
                case Card.CatapultRocks:
                    return new SplitPile(Card.CatapultRocks,Card.Catapult, Card.Rocks);
                case Card.GladiatorFortune:
                    return new SplitPile(Card.GladiatorFortune,Card.Gladiator, Card.Fortune);
                case Card.Castles:
                    if (_numberOfPlayers > 2)
                    {
                        return new Pile(Card.Castles, new List<Card>
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
                        return new Pile(Card.Castles, new List<Card>
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
                    return new SplitPile(Card.SaunaAvanto,Card.Sauna, Card.Avanto);
                
                
                default:
                    return new Pile(card, 10);
            }
            
        }
        

        public IEnumerable<IPile> Create(IEnumerable<Card> cards)
        {
            return cards.Select(Create).ToList();;
        }
    }
}