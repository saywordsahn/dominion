using System.Linq;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class OtherPlayersGainCard : IAbility
    {
        public Card Card { get; set; }
        public OtherPlayersGainCard(Card card)
        {
            Card = card;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            var otherPlayers = game.Players.Where(p => p != player);

            foreach (var otherPlayer in otherPlayers)
            {
                if (!game.Supply.Contains(Card)) break;
                game.Supply.Take(Card);
                otherPlayer.Gain(Card);
            }
            
            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}