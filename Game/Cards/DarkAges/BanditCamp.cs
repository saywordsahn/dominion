using System;
using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.DarkAges
{
    public class BanditCamp : ICard, IAction, IRulesHolder
    {
        public Card Name { get; } = Card.BanditCamp;
        public int Cost { get; } = 5;
        public CardType CardType { get; } = CardType.Action;

        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            return new List<IRule>
            {
                new GainCard(Card.Spoils, 1),
                new PlusActions(2),
                new PlusCards(1),
                
            };
        }
    }
}