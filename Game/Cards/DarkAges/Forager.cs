using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.CardSpecificAbilities;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.DarkAges
{
	public class Forager : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Forager;
		public int Cost { get; } = 3;
		public CardType CardType { get; } = CardType.Action;


		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
            return new List<IRule>
            {
                new ForagerAbility(),
                new TrashFromHand(new NoFilter()),
                new PlusBuys(1),
                new PlusActions(1)
            };
		}
	}
}