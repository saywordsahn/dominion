using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.Conditions;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Prosperity
{
	public class City : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.City;
		public int Cost { get; } = 5;
		public CardType CardType { get; } = CardType.Action;


		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new ConditionalAbility(new EmptySupplyPileCondition(2), new PlusMoney(1)),
				new ConditionalAbility(new EmptySupplyPileCondition(2), new PlusBuys(1)),
				new ConditionalAbility(new EmptySupplyPileCondition(1), new PlusCards(1)),
				new PlusActions(2),
				new PlusCards(1)
			};
		}
	}
}