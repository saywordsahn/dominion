using System;
using System.Linq;

namespace DominionWeb.Game.Cards.Base
{
    public class CouncilRoom : ICard, IAction
    {
        public int Cost { get; } = 5;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.CouncilRoom;

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.Draw(4);
            player.NumberOfBuys++;
            
            foreach (var otherPlayer in game.Players.Where(x => x != player))
            {
                otherPlayer.Draw(1);
            }
        }
    }
}