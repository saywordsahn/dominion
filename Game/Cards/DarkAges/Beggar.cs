using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.Reactions;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.DarkAges
{
    public class Beggar : ICard, IAction, IReaction, IAttackReaction, IRulesHolder
    {
        public Card Name { get; } = Card.Beggar;
        public int Cost { get; } = 2;
        public CardType CardType { get; } = CardType.Action;
        
        public void Resolve(Game game)
        {
            throw new NotImplementedException("Depreciating this method in favor of rules/abilities");
        }

        public IRule ReactionEffect() => new DiscardCardForTwoSilvers();

        public IEnumerable<IRule> GetRules(Game game, IPlayer player) => 
            new List<IRule> { new GainCardToHand(Card.Copper, 3) };
        
        
    }
}