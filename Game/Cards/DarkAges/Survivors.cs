using System;
using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.DarkAges
{
    public class Survivors : ICard, IAction, IRulesHolder, IRuins
    {
        public Card Name { get; } = Card.Survivors;
        public int Cost { get; } = 0;
        public CardType CardType { get; } = CardType.Action;
        public void Resolve(Game game)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}