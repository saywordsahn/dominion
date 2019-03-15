using System;
using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.Attacks;
using DominionWeb.Game.Cards.Abilities.Attacks.Effects;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Base
{
    public class Militia : ICard, IAction, IAttack, IRulesHolder
    {
        public int Cost { get; } = 4;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Militia;

        private bool PlayerCanBeAffected(IPlayer player)
        {
            //TODO: implement check from duration cards
            return player.Hand.Count > 3;
        }

        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            return new List<IRule>
            {
                new MilitiaAttack(),
                new PlusMoney(2)
            };
        }
    }
}