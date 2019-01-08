using System.Linq;
using DominionWeb.Game.Cards;
using DominionWeb.Game.Supply;

namespace DominionWeb.Game
{
    public class PlayerInitializer
    {
        private readonly Game _game;
        private readonly ISupply _supply;
        
        public PlayerInitializer(Game game, ISupply supply)
        {
            _game = game;
            _supply = supply;
        }

        public void Initialize()
        {            
            bool first = true;

            int numberOfCoppers = 7;

            var heirlooms = _supply.KingdomSupply
                .Select(x => x.PileCard)
                .Where(x => CardLists.Heirlooms.ContainsKey(x))
                .Select(x => CardLists.Heirlooms[x])
                .ToList();

            numberOfCoppers -= heirlooms.Count();
            
            foreach (var player in _game.Players)
            {
                heirlooms.ForEach(x => player.Gain(x));
                
                var coppers = Enumerable.Repeat(_supply.Take(Card.Copper), numberOfCoppers);
                var estates = Enumerable.Repeat(_supply.Take(Card.Estate), 3);
//                var witches = Enumerable.Repeat(Card.Seer, 3);
//                var thrones = Enumerable.Repeat(Card.KingsCourt, 3);
                player.Gain(coppers);
//                Console.WriteLine("{0} starts with 7 Coppers.", player.Name);
//                Console.WriteLine("{0} starts with 3 Estates.", player.Name);
                player.Gain(estates);
//                player.Gain(witches);
//                player.Gain(thrones);
                player.Shuffle();
//                Console.WriteLine("{0} shuffles their deck.", player.Name);
                player.Draw(5);

                if (first)
                {
                    player.PlayStatus = PlayStatus.BuyPhase;
                    player.NumberOfBuys = 1;
                    first = false;
                }
                else
                {
                    player.PlayStatus = PlayStatus.WaitForTurn;
                }
            }
        }
    }
}