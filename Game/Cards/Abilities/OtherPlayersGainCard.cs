using System.Linq;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class OtherPlayersGainCard : IAbility
    {
        public void Resolve(Game game, IPlayer player)
        {
            var otherPlayers = game.Players.Where(p => p != player);

            foreach (var otherPlayer in otherPlayers)
            {
                otherPlayer.Draw(1);
            }
            
            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}