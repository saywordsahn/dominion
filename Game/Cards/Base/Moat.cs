using System;

namespace DominionWeb.Game.Cards.Base
{
    public class Moat : ICard, IAction, IReaction
    {
        public int Cost { get; } = 2;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Moat;

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.Draw(2);
        }

        public void ReactionEffect(Game game)
        {
            throw new NotImplementedException();
        }
    }
}