using System;

namespace DominionWeb.Game.Cards.Base
{
    public class Market : ICard, IAction
    {
        public int Cost { get; } = 5;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Market;

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.Draw(1);
            player.NumberOfActions++;
            player.NumberOfBuys++;
            player.MoneyPlayed++;
        }
    }
}