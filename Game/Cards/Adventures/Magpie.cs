using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.Conditions;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Adventures
{
	public class Magpie : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Magpie;
		public int Cost { get; } = 4;
		public CardType CardType { get; } = CardType.Action;


		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				// we may need to change the PlusCards event because magpie does not specify +cards specifically
				// and triggers may be affected by this
				//TODO: redo conditional ability so action or victory cards are only revealed once
				new ConditionalAbility(new RevealTopCardCondition(new TreasureFilter()), new PlusCards(1),
					new ConditionalAbility(new RevealTopCardCondition(
						new OrFilter(new ActionFilter(), new VictoryFilter())), new GainCard(Card.Magpie))),
				new PlusCards(1),
				new PlusActions(1)
			};
		}
	}
}