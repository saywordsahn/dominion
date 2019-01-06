using System;
using System.Collections.Generic;
using DominionWeb.Game.Cards.Adventures;
using DominionWeb.Game.Cards.Alchemy;
using DominionWeb.Game.Cards.Base;
using DominionWeb.Game.Cards.Cornucopia;
using DominionWeb.Game.Cards.DarkAges;
using DominionWeb.Game.Cards.Empires;
using DominionWeb.Game.Cards.Guilds;
using DominionWeb.Game.Cards.Hinterlands;
using DominionWeb.Game.Cards.Intrigue;
using DominionWeb.Game.Cards.Nocturne;
using DominionWeb.Game.Cards.Prosperity;
using DominionWeb.Game.Cards.Renaissance;
using DominionWeb.Game.Cards.Seaside;

namespace DominionWeb.Game.Cards
{

    public static class CardFactory
    {
        //could use an array since we have an enum for all cards
        private static readonly Dictionary<Card, Func<ICard>> _dictionary = new Dictionary<Card, Func<ICard>>() {
            //base
            { Card.Copper, () => new Copper() },
            { Card.Silver, () => new Silver() },
            { Card.Gold, () => new Gold() },
            { Card.Estate, () => new Estate() },
            { Card.Duchy, () => new Duchy() },
            { Card.Province, () => new Province() },
            { Card.Curse, () => new Curse() },
            { Card.Cellar, () => new Cellar() },
            { Card.Chapel, () => new Chapel() },
            { Card.Moat, () => new Moat() },
            { Card.Harbinger, () => new Harbinger() },
            { Card.Merchant, () => new Merchant() },
            { Card.Vassal, () => new Vassal() },
            { Card.Village, () => new Village() },
            { Card.Workshop, () => new Workshop() },
            { Card.Bureaucrat, () => new Bureaucrat() },
            { Card.Gardens, () => new Gardens() },
            { Card.Militia, () => new Militia() },
            { Card.Moneylender, () => new Moneylender() },
            { Card.Poacher, () => new Poacher() },
            { Card.Remodel, () => new Remodel() },
            { Card.Smithy, () => new Smithy() },
            { Card.ThroneRoom, () => new ThroneRoom() },
            { Card.Bandit, () => new Bandit() },
            { Card.CouncilRoom, () => new CouncilRoom() },
            { Card.Festival, () => new Festival() },
            { Card.Laboratory, () => new Laboratory() },
            { Card.Library, () => new Library() },
            { Card.Market, () => new Market() },
            { Card.Mine, () => new Mine() },
            { Card.Sentry, () => new Sentry() },
            { Card.Witch, () => new Witch() },
            { Card.Artisan, () => new Artisan() },
            
            //intrigue
            { Card.Courtyard, () => new Courtyard() },
            { Card.Torturer, () => new Torturer() },
            { Card.Harem, () => new Harem() },
            { Card.Baron, () => new Baron() },
            { Card.Bridge, () => new Bridge() },
            { Card.Conspirator, () => new Conspirator() },
            { Card.Ironworks, () => new Ironworks() },
            { Card.Masquerade, () => new Masquerade() },
            { Card.MiningVillage, () => new MiningVillage() },
            { Card.Minion, () => new Minion() },
            { Card.Pawn, () => new Pawn() },
            { Card.ShantyTown, () => new ShantyTown() },
            { Card.Steward, () => new Steward() },
            { Card.Swindler, () => new Swindler() },
            { Card.TradingPost, () => new TradingPost() },
            { Card.WishingWell, () => new WishingWell() },
            { Card.Upgrade, () => new Upgrade() },
            { Card.Courtier, () => new Courtier() },
            { Card.Diplomat, () => new Diplomat() },
            { Card.Lurker, () => new Lurker() },
            { Card.Mill, () => new Mill() },
            { Card.Patrol, () => new Patrol() },
            { Card.Replace, () => new Replace() },
            { Card.SecretPassage, () => new SecretPassage() },
            
            //seaside
            { Card.Outpost, () => new Outpost() },
            { Card.GhostShip, () => new GhostShip() },
            { Card.Salvager, () => new Salvager() },
            { Card.PirateShip, () => new PirateShip() },
            { Card.NativeVillage, () => new NativeVillage() },
            { Card.Island, () => new Island() },
            { Card.Cutpurse, () => new Cutpurse() },
            { Card.Bazaar, () => new Bazaar() },
            { Card.Smugglers, () => new Smugglers() },
            { Card.Explorer, () => new Explorer() },
            { Card.PearlDiver, () => new PearlDiver() },
            { Card.TreasureMap, () => new TreasureMap() },
            { Card.Navigator, () => new Navigator() },
            { Card.Treasury, () => new Treasury() },
            { Card.Lookout, () => new Lookout() },
            { Card.Ambassador, () => new Ambassador() },
            { Card.Warehouse, () => new Warehouse() },
            { Card.Embargo, () => new Embargo() },
            { Card.Haven, () => new Haven() },
            { Card.SeaHag, () => new SeaHag() },
            { Card.Tactician, () => new Tactician() },
            { Card.Caravan, () => new Caravan() },
            { Card.Lighthouse, () => new Lighthouse() },
            { Card.FishingVillage, () => new FishingVillage() },
            { Card.Wharf, () => new Wharf() },
            { Card.MerchantShip, () => new MerchantShip() },
            
            //prosperity
            { Card.Hoard, () => new Hoard() },
            { Card.KingsCourt, () => new KingsCourt() },
            { Card.Loan, () => new Loan() },
            { Card.Mint, () => new Mint() },
            { Card.Monument, () => new Monument() },
            { Card.Mountebank, () => new Mountebank() },
            { Card.Peddler, () => new Peddler() },
            { Card.Quarry, () => new Quarry() },
            { Card.Rabble, () => new Rabble() },
            { Card.RoyalSeal, () => new RoyalSeal() },
            { Card.Talisman, () => new Talisman() },
            { Card.TradeRoute, () => new TradeRoute() },
            { Card.Vault, () => new Vault() },
            { Card.Venture, () => new Venture() },
            { Card.WatchTower, () => new WatchTower() },
            { Card.WorkersVillage, () => new WorkersVillage() },
            { Card.Colony, () => new Colony() },
            { Card.Platinum, () => new Platinum() },
            { Card.Bank, () => new Bank() },
            { Card.Bishop, () => new Bishop() },
            { Card.City, () => new City() },
            { Card.Contraband, () => new Contraband() },
            { Card.CountingHouse, () => new CountingHouse() },
            { Card.Expand, () => new Expand() },
            { Card.Forge, () => new Forge() },
            { Card.Goons, () => new Goons() },
            { Card.GrandMarket, () => new GrandMarket() },
            
            //alchemy
            { Card.Alchemist, () => new Alchemist() },
            { Card.Apothecary, () => new Apothecary() },
            { Card.Apprentice, () => new Apprentice() },
            { Card.Familiar, () => new Familiar() },
            { Card.Golem, () => new Golem() },
            { Card.Herbalist, () => new Herbalist() },
            { Card.PhilosophersStone, () => new PhilosophersStone() },
            { Card.Possession, () => new Possession() },
            { Card.ScryingPool, () => new ScryingPool() },
            //{ Card.Transmute, () => new Transmute() },
            { Card.University, () => new University() },
            { Card.Vineyard, () => new Vineyard() },
            
            //cornucopia
            { Card.HornofPlenty, () => new HornofPlenty() },
            { Card.Fairgrounds, () => new Fairgrounds() },
            { Card.FarmingVillage, () => new FarmingVillage() },
            { Card.FortuneTeller, () => new FortuneTeller() },
            { Card.Hamlet, () => new Hamlet() },
            { Card.Harvest, () => new Harvest() },
            { Card.HorseTraders, () => new HorseTraders() },
            { Card.HuntingParty, () => new HuntingParty() },
            { Card.Jester, () => new Jester() },
            { Card.Menagerie, () => new Menagerie() },
            { Card.Remake, () => new Remake() },
            { Card.Tournament, () => new Tournament() },
            { Card.YoungWitch, () => new YoungWitch() },
//            { Card.BagofGold, () => new BagofGold() },
//            { Card.Diadem, () => new Diadem() },
//            { Card.Followers, () => new Followers() },
//            { Card.Princess, () => new Princess() },
//            { Card.TrustySteed, () => new TrustySteed() },
            
            //hinterlands
            { Card.BorderVillage, () => new BorderVillage() },
            { Card.Cache, () => new Cache() },
            { Card.Cartographer, () => new Cartographer() },
            { Card.Crossroads, () => new Crossroads() },
            { Card.Develop, () => new Develop() },
            { Card.Duchess, () => new Duchess() },
            { Card.Embassy, () => new Embassy() },
            { Card.Farmland, () => new Farmland() },
            { Card.FoolsGold, () => new FoolsGold() },
            { Card.Haggler, () => new Haggler() },
            { Card.Highway, () => new Highway() },
            { Card.IllGottenGains, () => new IllGottenGains() },
            { Card.Inn, () => new Inn() },
            { Card.JackofallTrades, () => new JackofallTrades() },
            { Card.Mandarin, () => new Mandarin() },
            { Card.Margrave, () => new Margrave() },
            { Card.NobleBrigand, () => new NobleBrigand() },
            { Card.NomadCamp, () => new NomadCamp() },
            { Card.Oasis, () => new Oasis() },
            { Card.Oracle, () => new Oracle() },
            { Card.Scheme, () => new Scheme() },
            { Card.SilkRoad, () => new SilkRoad() },
            { Card.SpiceMerchant, () => new SpiceMerchant() },
            { Card.Stables, () => new Stables() },
            { Card.Trader, () => new Trader() },
            { Card.Tunnel, () => new Tunnel() },
            
            //dark ages
            { Card.AbandonedMine, () => new AbandonedMine() },
            { Card.Altar, () => new Altar() },
            { Card.Armory, () => new Armory() },
            { Card.BanditCamp, () => new BanditCamp() },
            { Card.BandOfMisfits, () => new BandOfMisfits() },
            { Card.Beggar, () => new Beggar() },
            { Card.Catacombs, () => new Catacombs() },
            { Card.Count, () => new Count() },
            { Card.Counterfeit, () => new Counterfeit() },
            { Card.Cultist, () => new Cultist() },
            { Card.DeathCart, () => new DeathCart() },
            { Card.Feodum, () => new Feodum() },
            { Card.Forager, () => new Forager() },
            { Card.Fortress, () => new Fortress() },
            { Card.Graverobber, () => new Graverobber() },
            { Card.Hermit, () => new Hermit() },
            { Card.HuntingGrounds, () => new HuntingGrounds() },
            { Card.Ironmonger, () => new Ironmonger() },
            { Card.JunkDealer, () => new JunkDealer() },
            { Card.Madman, () => new Madman() },
            { Card.Marauder, () => new Marauder() },
            { Card.MarketSquare, () => new MarketSquare() },
            { Card.Mercenary, () => new Mercenary() },
            { Card.Mystic, () => new Mystic() },
            { Card.Pillage, () => new Pillage() },
            { Card.PoorHouse, () => new PoorHouse() },
            { Card.Procession, () => new Procession() },
            { Card.Rats, () => new Rats() },
            { Card.Rebuild, () => new Rebuild() },
            { Card.Rogue, () => new Rogue() },
            { Card.Sage, () => new Sage() },
            { Card.Scavenger, () => new Scavenger() },
            { Card.Spoils, () => new Spoils() },
            { Card.Squire, () => new Squire() },
            { Card.Storeroom, () => new Storeroom() },
            { Card.WanderingMinstrel, () => new WanderingMinstrel() },
            { Card.RuinedLibrary, () => new RuinedLibrary() },
            { Card.RuinedMarket, () => new RuinedMarket() },
            { Card.RuinedVillage, () => new RuinedVillage() },
            { Card.Survivors, () => new Survivors() },
            { Card.Urchin, () => new Urchin() },
            { Card.Vagrant, () => new Vagrant() },
            { Card.DameAnna, () => new DameAnna() },
            { Card.DameJosephine, () => new DameJosephine() },
            { Card.DameMolly, () => new DameMolly() },
            { Card.DameNatalie, () => new DameNatalie() },
            { Card.DameSylvia, () => new DameSylvia() },
            { Card.SirBailey, () => new SirBailey() },
            { Card.SirDestry, () => new SirDestry() },
            { Card.SirMartin, () => new SirMartin() },
            { Card.SirMichael, () => new SirMichael() },
            { Card.SirVander, () => new SirVander() },
            { Card.VirtualRuins, () => new VirtualRuins() },
            
            //guilds
            { Card.Advisor, () => new Advisor() },
            { Card.Baker, () => new Baker() },
            { Card.Butcher, () => new Butcher() },
            { Card.CandlestickMaker, () => new CandlestickMaker() },
            { Card.Doctor, () => new Doctor() },
            { Card.Herald, () => new Herald() },
            { Card.Journeyman, () => new Journeyman() },
            { Card.Masterpiece, () => new Masterpiece() },
            { Card.MerchantGuild, () => new MerchantGuild() },
            { Card.Plaza, () => new Plaza() },
            { Card.StoneMason, () => new StoneMason() },
            { Card.Soothsayer, () => new Soothsayer() },
            { Card.Taxman, () => new Taxman() },
            
            //adventures
            { Card.Ratcatcher, () => new Ratcatcher() },
            { Card.Raze, () => new Raze() },
            { Card.Amulet, () => new Amulet() },
            { Card.CaravanGuard, () => new CaravanGuard() },
            { Card.Dungeon, () => new Dungeon() },
            { Card.Gear, () => new Gear() },
            { Card.Guide, () => new Guide() },
            { Card.Duplicate, () => new Duplicate() },
            { Card.Magpie, () => new Magpie() },
            { Card.Messenger, () => new Messenger() },
            { Card.Miser, () => new Miser() },
            { Card.Port, () => new Port() },
            { Card.Ranger, () => new Ranger() },
            { Card.Transmogrify, () => new Transmogrify() },
            { Card.Artificer, () => new Artificer() },
            { Card.BridgeTroll, () => new BridgeTroll() },
            { Card.DistantLands, () => new DistantLands() },
            { Card.Giant, () => new Giant() },
            { Card.HauntedWoods, () => new HauntedWoods() },
            { Card.LostCity, () => new LostCity() },
            { Card.Relic, () => new Relic() },
            { Card.RoyalCarriage, () => new RoyalCarriage() },
            { Card.Storyteller, () => new Storyteller() },
            { Card.SwampHag, () => new SwampHag() },
            { Card.TreasureTrove, () => new TreasureTrove() },
            { Card.WineMerchant, () => new WineMerchant() },
            { Card.Hireling, () => new Hireling() },
            { Card.Soldier, () => new Soldier() },
            { Card.TreasureHunter, () => new TreasureHunter() },
            { Card.Fugitive, () => new Fugitive() },
            { Card.Warrior, () => new Warrior() },
            { Card.Disciple, () => new Disciple() },
            { Card.Hero, () => new Hero() },
            { Card.Champion, () => new Champion() },
            { Card.Teacher, () => new Teacher() },

            
            //empires
            { Card.Emporium, () => new Emporium() },
            { Card.Encampment, () => new Encampment() },
            { Card.Enchantress, () => new Enchantress() },
            { Card.Engineer, () => new Engineer() },
            { Card.FarmersMarket, () => new FarmersMarket() },
            { Card.Fortune, () => new Fortune() },
            { Card.Forum, () => new Forum() },
            { Card.Gladiator, () => new Gladiator() },
            { Card.Groundskeeper, () => new Groundskeeper() },
            { Card.Legionary, () => new Legionary() },
            { Card.Overlord, () => new Overlord() },
            { Card.Patrician, () => new Patrician() },
            { Card.Plunder, () => new Plunder() },
            { Card.Rocks, () => new Rocks() },
            { Card.RoyalBlacksmith, () => new RoyalBlacksmith() },
            { Card.Sacrifice, () => new Sacrifice() },
            { Card.Settlers, () => new Settlers() },
            { Card.Temple, () => new Temple() },
            { Card.Villa, () => new Villa() },
            { Card.WildHunt, () => new WildHunt() },
            { Card.HumbleCastle, () => new HumbleCastle() },
            { Card.CrumblingCastle, () => new CrumblingCastle() },
            { Card.SmallCastle, () => new SmallCastle() },
            { Card.HauntedCastle, () => new HauntedCastle() },
            { Card.OpulentCastle, () => new OpulentCastle() },
            { Card.SprawlingCastle, () => new SprawlingCastle() },
            { Card.GrandCastle, () => new GrandCastle() },
            { Card.KingsCastle, () => new KingsCastle() },
            { Card.CatapultRocks, () => new CatapultRocks() },
            { Card.Castles, () => new Castles() },
            { Card.EncampmentPlunder, () => new EncampmentPlunder() },
            { Card.GladiatorFortune, () => new GladiatorFortune() },
            { Card.PatricianEmporium, () => new PatricianEmporium() },
            { Card.SettlersBustlingVillage, () => new SettlersBustlingVillage() },
            
            //nocturne
            { Card.Exorcist, () => new Exorcist() },
            { Card.FaithfulHound, () => new FaithfulHound() },
            { Card.Fool, () => new Fool() },
            { Card.Ghost, () => new Ghost() },
            { Card.GhostTown, () => new GhostTown() },
            { Card.Goat, () => new Goat() },
            { Card.Guardian, () => new Guardian() },
            { Card.HauntedMirror, () => new HauntedMirror() },
            { Card.Idol, () => new Idol() },
            { Card.Imp, () => new Imp() },
            { Card.Leprechaun, () => new Leprechaun() },
            { Card.LuckyCoin, () => new LuckyCoin() },
            { Card.MagicLamp, () => new MagicLamp() },
            { Card.Monastery, () => new Monastery() },
            { Card.Necromancer, () => new Necromancer() },
            { Card.NightWatchman, () => new NightWatchman() },
            { Card.Pasture, () => new Pasture() },
            { Card.Pixie, () => new Pixie() },
            { Card.Pooka, () => new Pooka() },
            { Card.Pouch, () => new Pouch() },
            { Card.Raider, () => new Raider() },
            { Card.SacredGrove, () => new SacredGrove() },
            { Card.SecretCave, () => new SecretCave() },
            { Card.Shepherd, () => new Shepherd() },
            { Card.Skulk, () => new Skulk() },
            { Card.Tormentor, () => new Tormentor() },
            { Card.Tracker, () => new Tracker() },
            { Card.TragicHero, () => new TragicHero() },
            { Card.Vampire, () => new Vampire() },
            { Card.Werewolf, () => new Werewolf() },
            { Card.WillOWisp, () => new WillOWisp() },
            { Card.Wish, () => new Wish() },
            { Card.ZombieApprentice, () => new ZombieApprentice() },
            { Card.ZombieMason, () => new ZombieMason() },
            { Card.ZombieSpy, () => new ZombieSpy() },

            
            //renaissance
            { Card.BorderGuard, () => new BorderGuard() },
            { Card.Ducat, () => new Ducat() },
            { Card.Lackeys, () => new Lackeys() },
            { Card.ActingTroupe, () => new ActingTroupe() },
            { Card.CargoShip, () => new CargoShip() },
            { Card.Experiment, () => new Experiment() },
            { Card.Improve, () => new Improve() },
            { Card.FlagBearer, () => new FlagBearer() },
            { Card.Hideout, () => new Hideout() },
            { Card.Inventor, () => new Inventor() },
            { Card.MountainVillage, () => new MountainVillage() },
            { Card.Patron, () => new Patron() },
            { Card.Priest, () => new Priest() },
            { Card.Research, () => new Research() },
            { Card.SilkMerchant, () => new SilkMerchant() },
            { Card.OldWitch, () => new OldWitch() },
            { Card.Recruiter, () => new Recruiter() },
            { Card.Scepter, () => new Scepter() },
            { Card.Scholar, () => new Scholar() },
            { Card.Sculptor, () => new Sculptor() },
            { Card.Seer, () => new Seer() },
            { Card.Spices, () => new Spices() },
            { Card.Swashbuckler, () => new Swashbuckler() },
            { Card.Treasurer, () => new Treasurer() },
            { Card.Villain, () => new Villain() },
            
        };

        public static ICard Create(Card card)
        {
            return _dictionary[card]();
        }
        
        public static ICard Create(Card card, bool isThroned)
        {
            switch (card)
            {
                case Card.Experiment:
                    return new Experiment(false, isThroned);
                default:
                    return _dictionary[card]();
            }
        }

        public static int[] GetCardCostArray()
        {
            var cardCostArray = new int[_dictionary.Count];
            
            foreach (var c in _dictionary)
            {
                cardCostArray[(int)c.Key] = Create(c.Key).Cost;
            }

            return cardCostArray;
        }

    }
}