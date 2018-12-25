using System;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Base
{
    public class Moat : ICard, IAction, IAttackReaction, IAttackBlocker, IRuleHolder
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

        public IRule ReactionEffect()
        {
            //TODO: code smell
            throw new InvalidOperationException("This ability should not be called.");
        }

        public IRule GetRule(Game game, IPlayer player) => new PlusCards(2);
    }
}