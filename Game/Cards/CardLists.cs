using System.Collections.Generic;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2.HPack;

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

        public static Dictionary<Card, Card> Heirlooms = new Dictionary<Card, Card>
        {
            { Card.Pooka, Card.CursedGold },
            { Card.Pixie, Card.Goat },
            { Card.Tracker, Card.Pouch },
            { Card.Fool, Card.LuckyCoin },
            { Card.SecretCave, Card.MagicLamp },
            { Card.Cemetery, Card.HauntedMirror },
            { Card.Shepherd, Card.Pasture }
        };

        public static List<Card> AvailableCards = new List<Card>
        {
            Card.Artisan,
            //Card.Bandit,
            //Card.Bureaucrat,
            //Card.Cellar,
            Card.Chapel,
            Card.CouncilRoom,
            Card.Festival,
            Card.Gardens,
            Card.Harbinger,
            Card.Laboratory,
            //Card.Library,
            Card.Market,
            Card.Merchant,
            Card.Militia,
            //Card.Mine,
            Card.Moneylender,
            //Card.Poacher,
            Card.Remodel,
            //Card.Sentry,
            Card.Smithy,
            Card.ThroneRoom,
            Card.Vassal,
            Card.Village,
            Card.Witch,
            Card.Workshop,
            
//            Card.Courtyard,
//            Card.Torturer,
            Card.Harem,
//            Card.Baron,
//            Card.Bridge,
//            Card.Conspirator,
//            Card.Ironworks,
//            Card.Masquerade,
//            Card.MiningVillage,
//            Card.Minion,
            Card.Pawn,
//            Card.ShantyTown,
            Card.Steward,
//            Card.Swindler,
//            Card.TradingPost,
//            Card.WishingWell,
//            Card.Upgrade,
//            Card.Courtier,
//            Card.Diplomat,
            Card.Mill,
//            Card.Patrol,
//            Card.Replace,
//            Card.SecretPassage
            Card.Lurker,
            Card.Nobles,


//            Card.Hoard,
            Card.KingsCourt,
//            Card.Loan,
//            Card.Mint,
            Card.Monument,
//            Card.Mountebank,
//            Card.Peddler,
//            Card.Quarry,
//            Card.Rabble,
//            Card.RoyalSeal,
//            Card.Talisman,
//            Card.TradeRoute,
//            Card.Vault,
//            Card.Venture,
//            Card.WatchTower,
            Card.WorkersVillage,
//            Card.Colony,
//            Card.Platinum,
//            Card.Bank, Card.Bishop,
//             Card.City,
//             Card.Contraband,
         Card.CountingHouse,
         Card.Expand,
//            Card.Forge, Card.Goons, Card.GrandMarket
            


//
//            Card.Outpost,
//            Card.GhostShip,
//            Card.Salvager,
//            Card.PirateShip,
//            Card.NativeVillage,
            Card.Island,
//            Card.Cutpurse,
            Card.Bazaar,
//            Card.Smugglers,
//            Card.Explorer,
//            Card.PearlDiver,
//            Card.TreasureMap,
//            Card.Navigator,
//            Card.Treasury,
//            Card.Lookout,
//            Card.Ambassador,
//            Card.Warehouse,
//            Card.Embargo,
//            Card.Haven,
//             Card.SeaHag,
//             Card.Tactician,
//             Card.Caravan,
             Card.Lighthouse,
//             Card.FishingVillage,
             Card.Wharf,
//             Card.MerchantShip,


//
//            Card.Alchemist,
//            Card.Apothecary,
//            Card.Apprentice,
//            Card.Familiar,
//            Card.Golem,
//            Card.Herbalist,
//            Card.PhilosophersStone,
//            Card.Possession,
//            Card.ScryingPool,
//            Card.Transmute,
//            Card.University,
//            Card.Vineyard


//
//            Card.HornofPlenty,
//            Card.Fairgrounds,
//            Card.FarmingVillage,
//            Card.FortuneTeller,
//            Card.Hamlet,
//            Card.Harvest,
//            Card.HorseTraders,
//            Card.HuntingParty,
//            Card.Jester,
//            Card.Menagerie,
//            Card.Remake,
//            Card.Tournament,
//            Card.YoungWitch,
//            Card.BagofGold,
//            Card.Diadem,
//            Card.Followers,
//            Card.Princess,
//            Card.TrustySteed



//            Card.BorderVillage,
//            Card.Cache,
//            Card.Cartographer,
//            Card.Crossroads,
//            Card.Develop,
//            Card.Duchess,
            Card.Embassy,
//            Card.Farmland,
//            Card.FoolsGold,
//            Card.Haggler,
//            Card.Highway,
//            Card.IllGottenGains,
//            Card.Inn,
//            Card.JackofallTrades,
//            Card.Mandarin,
//            Card.Margrave,
//            Card.NobleBrigand,
            Card.NomadCamp,
            Card.Oasis,
//            Card.Oracle,
//            Card.Scheme,
            Card.SilkRoad,
//            Card.SpiceMerchant,
//            Card.Stables,
//            Card.Trader,
//            Card.Tunnel



            Card.Altar,
            Card.Armory,
//            Card.BandOfMisfits,
            Card.BanditCamp,
            Card.Beggar,
//            Card.Catacombs,
//            Card.Count,
//            Card.Counterfeit,
            Card.DeathCart,
//            Card.Feodum,
            Card.Forager,
//            Card.Fortress,
//            Card.Graverobber,
//            Card.HuntingGrounds,
//            Card.Ironmonger,
            Card.JunkDealer,
//            Card.MarketSquare,
//            Card.Mystic,
//            Card.Pillage,
            Card.PoorHouse,
//            Card.Procession,
//            Card.Rats,
//            Card.Rebuild,
//            Card.Rogue,
//            Card.Sage,
            Card.Scavenger,
//            Card.Spoils,
//            Card.Squire,
//            Card.Storeroom,
//            Card.WanderingMinstrel,
//            Card.Cultist,
//            Card.Urchin,
//            Card.Mercenary,
            Card.Marauder,
//            Card.Hermit,
//            Card.Madman,
//            Card.Vagrant,



//            Card.Advisor,
//            Card.Baker,
//            Card.Butcher,
//            Card.CandlestickMaker,
//            Card.Doctor,
//            Card.Herald,
//            Card.Journeyman,
//            Card.Masterpiece,
//            Card.MerchantGuild,
//            Card.Plaza,
//            Card.StoneMason,
//            Card.Soothsayer,
//            Card.Taxman


//
//            Card.CoinOfTheRealm,
//            Card.Page,
//            Card.Peasant,
//            Card.Ratcatcher,
//            Card.Raze,
//            Card.Amulet,
//            Card.CaravanGuard,
//            Card.Dungeon,
//            Card.Gear,
//            Card.Guide,
//            Card.Duplicate,
            Card.Magpie,
//            Card.Messenger,
            Card.Miser,
            Card.Port,
            Card.Ranger,
//            Card.Transmogrify,
//            Card.Artificer,
//            Card.BridgeTroll,
//            Card.DistantLands,
//            Card.Giant,
//            Card.HauntedWoods,
//            Card.LostCity,
//            Card.Relic,
//            Card.RoyalCarriage,
//            Card.Storyteller,
//            Card.SwampHag,
//            Card.TreasureTrove,
//            Card.WineMerchant,
//            Card.Hireling,
//            Card.Soldier,
//            Card.TreasureHunter,
//            Card.Fugitive,
//            Card.Warrior,
//            Card.Disciple,
//            Card.Hero,
//            Card.Champion,
//            Card.Teacher,


//            Card.Archive,
//            Card.Capital,
//            Card.Catapult,
//            Card.ChariotRace,
//            Card.Charm,
//            Card.CityQuarter,
//            Card.Crown,
//            Card.Encampment,
//            Card.Enchantress,
//            Card.Engineer,
//            Card.FarmersMarket,
//            Card.Fortune,
            Card.Forum,
//            Card.Gladiator,
            Card.Groundskeeper,
//            Card.Legionary,
//            Card.Overlord,
            Card.Plunder,
//            Card.Rocks,
//            Card.RoyalBlacksmith,
//            Card.Sacrifice,
//            Card.Temple,
//            Card.Villa,
//            Card.WildHunt,
//            Card.HumbleCastle,
//            Card.CrumblingCastle,
//            Card.SmallCastle,
//            Card.HauntedCastle,
//            Card.OpulentCastle,
//            Card.SprawlingCastle,
//            Card.GrandCastle,
//            Card.KingsCastle,
//            Card.CatapultRocks,
//            Card.Castles,
//            Card.EncampmentPlunder,
//            Card.GladiatorFortune,
            Card.PatricianEmporium,
            Card.SettlersBustlingVillage,

            
            
//            Card.Alms,
//            Card.Borrow,
//            Card.Quest,
//            Card.Save,
//            Card.ScoutingParty,
//            Card.TravellingFair,
//            Card.Bonfire,
//            Card.Expedition,
//            Card.Ferry,
//            Card.Plan,
//            Card.Mission,
//            Card.Pilgrimage,
//            Card.Ball,
//            Card.Raid,
//            Card.Seaway,
//            Card.Trade,
//            Card.LostArts,
//            Card.Training,
//            Card.Inheritance,
//            Card.Pathfinding,
//            Card.Triumph,
//            Card.Annex,
//            Card.Donate,
//            Card.Advance,
//            Card.Banquet,
//            Card.Conquest,
//            Card.Dominate,
//            Card.Delve,
//            Card.Ritual,
//            Card.SaltTheEarth,
//            Card.Tax,
//            Card.Wedding,
//            Card.Windfall


//
//            Card.Bard,
//            Card.Bat,
//            Card.BlessedVillage,
//            Card.Cemetery,
//            Card.Changeling,
//            Card.Cobbler,
//            Card.Conclave,
//            Card.Crypt,
//            Card.CursedGold,
//            Card.CursedVillage,
//            Card.DenOfSin,
//            Card.DevilsWorkshop,
//            Card.Druid,
//            Card.Exorcist,
//            Card.FaithfulHound,
//            Card.Fool,
//            Card.Ghost,
//            Card.GhostTown,
//            Card.Goat,
//            Card.Guardian,
//            Card.HauntedMirror,
//            Card.Idol,
//            Card.Imp,
//            Card.Leprechaun,
//            Card.LuckyCoin,
//            Card.MagicLamp,
//            Card.Monastery,
//            Card.Necromancer,
//            Card.NightWatchman,
//            Card.Pasture,
//            Card.Pixie,
            Card.Pooka,
//            Card.Pouch,
//            Card.Raider,
//            Card.SacredGrove,
//            Card.SecretCave,
//            Card.Shepherd,
//            Card.Skulk,
//            Card.Tormentor,
//            Card.Tracker,
//            Card.TragicHero,
//            Card.Vampire,
//            Card.Werewolf,
//            Card.WillOWisp,
//            Card.Wish,
//            Card.ZombieApprentice,
//            Card.ZombieMason,
//            Card.ZombieSpy





//            Card.BorderGuard,
            Card.Ducat,
            Card.Lackeys,
            Card.ActingTroupe,
//            Card.CargoShip,
            Card.Experiment,
//            Card.Improve,
//            Card.FlagBearer,
            Card.Hideout,
//            Card.Inventor,
//            Card.MountainVillage,
//            Card.Patron,
            Card.Priest,
//            Card.Research,
            Card.SilkMerchant,
//            Card.OldWitch,
//            Card.Recruiter,
//            Card.Scepter,
            Card.Scholar,
//            Card.Sculptor,
            Card.Seer,
            Card.Spices,
//            Card.Swashbuckler,
//            Card.Treasurer,
//            Card.Villain
        };

    }
}