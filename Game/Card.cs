namespace DominionWeb.Game
{
    public enum Card
    {
        Copper,
        Silver,
        Gold,
        Estate,
        Duchy,
        Province,
        Curse,        
        Cellar,
        Chapel,
        Moat,
        Harbinger,
        Merchant,
        Vassal,
        Village,
        Workshop,
        Bureaucrat,
        Gardens,
        Militia,
        Moneylender,
        Poacher,
        Remodel,
        Smithy,
        ThroneRoom,
        Bandit,
        CouncilRoom,
        Festival,
        Laboratory,
        Library,
        Market,
        Mine,
        Sentry,
        Witch,
        Artisan,
        
        
        // Intrigue Expansion
        Duke, SecretChamber, Nobles, Coppersmith, Courtyard, Torturer, Harem, Baron, Bridge,
        Conspirator, Ironworks, Masquerade, MiningVillage, Minion, Pawn, Saboteur, ShantyTown,
        Scout, Steward, Swindler, TradingPost, WishingWell, Upgrade, Tribute, GreatHall,
        // Intrigue Second Edition
        Courtier, Diplomat, Lurker, Mill, Patrol, Replace, SecretPassage,

        // Seaside Expansion
        Haven, SeaHag, Tactician, Caravan, Lighthouse, FishingVillage, Wharf, MerchantShip,
        Outpost, GhostShip, Salvager, PirateShip, NativeVillage, Island, Cutpurse, Bazaar,
        Smugglers, Explorer, PearlDiver, TreasureMap, Navigator, Treasury, Lookout, Ambassador,
        Warehouse, Embargo,
        // Alchemy Expansion
        Alchemist, Apothecary, Apprentice, Familiar, Golem, Herbalist, PhilosophersStone,
        Possession, ScryingPool, Transmute, University, Vineyard,
        // Prosperity Expansion
        Bank, Bishop, City, Contraband, CountingHouse, Expand, Forge, Goons, GrandMarket, Hoard,
        KingsCourt, Loan, Mint, Monument, Mountebank, Peddler, Quarry, Rabble, RoyalSeal, Talisman,
        TradeRoute, Vault, Venture, WatchTower, WorkersVillage,
        // Cornucopia Expansion
        HornofPlenty, Fairgrounds, FarmingVillage, FortuneTeller, Hamlet, Harvest, HorseTraders,
        HuntingParty, Jester, Menagerie, Remake, Tournament, YoungWitch, BagofGold, Diadem,
        Followers, Princess, TrustySteed,
        // Hinterlands Expansion
        BorderVillage, Cache, Cartographer, Crossroads, Develop, Duchess, Embassy, Farmland,
        FoolsGold, Haggler, Highway, IllGottenGains, Inn, JackofallTrades, Mandarin, Margrave,
        NobleBrigand, NomadCamp, Oasis, Oracle, Scheme, SilkRoad, SpiceMerchant, Stables, Trader,
        Tunnel,
        // Dark Ages Expansion
        Altar, Armory, BandOfMisfits, BanditCamp, Beggar, Catacombs, Count, Counterfeit, DeathCart,
        Feodum, Forager, Fortress, Graverobber, HuntingGrounds, Ironmonger, JunkDealer,
        MarketSquare, Mystic, Pillage, PoorHouse, Procession, Rats, Rebuild, Rogue, Sage,
        Scavenger, Spoils, Squire, Storeroom, WanderingMinstrel, Necropolis, Hovel,
        OvergrownEstate, AbandonedMine, RuinedLibrary, RuinedMarket, RuinedVillage, Survivors,
        Cultist, Urchin, Mercenary, Marauder, Hermit, Madman, Vagrant, DameAnna, DameJosephine,
        DameMolly, DameNatalie, DameSylvia, SirBailey, SirDestry, SirMartin, SirMichael, SirVander,
        VirtualRuins, VirtualKnight,
        // Guilds Expansion
        Advisor, Baker, Butcher, CandlestickMaker, Doctor, Herald, Journeyman, Masterpiece, MerchantGuild, Plaza, StoneMason, Soothsayer, Taxman,

        // Adventures Expansion
        CoinOfTheRealm, Page, Peasant, Ratcatcher, Raze, Amulet, CaravanGuard, Dungeon, Gear, Guide,
        Duplicate, Magpie, Messenger, Miser, Port, Ranger, Transmogrify, Artificer, BridgeTroll, DistantLands,
        Giant, HauntedWoods, LostCity, Relic, RoyalCarriage, Storyteller, SwampHag, TreasureTrove, WineMerchant, Hireling, 
        
        Soldier, TreasureHunter, Fugitive, Warrior, Disciple, Hero, Champion, Teacher, 
        
        Alms, Borrow, Quest, Save, ScoutingParty, TravellingFair, Bonfire, Expedition, Ferry, Plan, Mission, Pilgrimage,
        Ball, Raid, Seaway, Trade, LostArts, Training, Inheritance, Pathfinding,
        
        // Empires Expansion
        Archive, BustlingVillage, Capital, Catapult, ChariotRace, Charm, CityQuarter, Crown, Emporium, Encampment, Enchantress, 
        Engineer, FarmersMarket, Fortune, Forum, Gladiator, Groundskeeper, Legionary, Overlord, Patrician, Plunder, Rocks, 
        RoyalBlacksmith, Sacrifice, Settlers, Temple, Villa, WildHunt,
        HumbleCastle, CrumblingCastle, SmallCastle, HauntedCastle, OpulentCastle, SprawlingCastle, GrandCastle, KingsCastle,
        
        CatapultRocks, Castles, EncampmentPlunder, GladiatorFortune, PatricianEmporium, SettlersBustlingVillage,
        
        Advance, Annex, Banquet, Conquest, Dominate, Delve, Donate, Ritual, SaltTheEarth, Tax, Triumph, Wedding, Windfall,
        Aqueduct, Arena, BanditFort, Basilica, Baths, Battlefield, Colonnade, DefiledShrine, Fountain, Keep, Labyrinth, MountainPass, Museum, Obelisk, Orchard, Palace, Tomb, Tower, TriumphalArch, Wall, WolfDen,
        
        // Nocturne Expansion
        Bard, Bat, BlessedVillage, Cemetery, Changeling, Cobbler, Conclave, Crypt, CursedGold, CursedVillage, DenOfSin, DevilsWorkshop,
        Druid, Exorcist, FaithfulHound, Fool, Ghost, GhostTown, Goat, Guardian, HauntedMirror, Idol, Imp, Leprechaun, LuckyCoin, MagicLamp, 
        Monastery, Necromancer, NightWatchman, Pasture, Pixie, Pooka, Pouch, Raider, SacredGrove, SecretCave, Shepherd, Skulk, Tormentor, 
        Tracker, TragicHero, Vampire, Werewolf, WillOWisp, Wish, ZombieApprentice, ZombieMason, ZombieSpy,
        
        TheEarthsGift, TheFieldsGift, TheFlamesGift, TheForestsGift, TheMoonsGift, TheMountainsGift, TheRiversGift, TheSeasGift,
        TheSkysGift, TheSunsGift, TheSwampsGift, TheWindsGift,
        
        BadOmens, Delusion, Envy, Famine, Fear, Greed, Haunting, Locusts, Misery, Plague, Poverty, War,
        
        Deluded, Envious, LostInTheWoods, Miserable, TwiceMiserable,
        
        //Renaissance Expansion
        BorderGuard, Ducat, Lackeys, ActingTroupe, CargoShip, Experiment, Improve, FlagBearer, Hideout, Inventor,
        MountainVillage, Patron, Priest, Research, SilkMerchant, OldWitch, Recruiter, Scepter, Scholar, Sculptor,
        Seer, Spices, Swashbuckler, Treasurer, Villain,
        
        
        
        //not playable
        Any
    }
}
