using System;
using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Base
{
    public class Moat : ICard, IAction, IAttackReaction, IAttackBlocker, IRulesHolder
    {
        public int Cost { get; } = 2;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Moat;

        public void ReactionEffect(Game game)
        {
            throw new NotImplementedException();
        }

        public IRule ReactionEffect()
        {
            //TODO: code smell
            throw new InvalidOperationException("This ability should not be called.");
        }

        public IEnumerable<IRule> GetRules(Game game, IPlayer player) => 
            new List<IRule> { new PlusCards(2) };
    }
}