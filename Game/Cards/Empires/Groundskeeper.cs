using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.TriggeredAbilities;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;
using DominionWeb.Game.Player.Triggers;

namespace DominionWeb.Game.Cards.Empires
{
	public class Groundskeeper : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Groundskeeper;
		public int Cost { get; } = 5;
		public CardType CardType { get; } = CardType.Action;

		public void Resolve(Game game)
		{
			
		}

		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new AddTriggeredAbility(
					new TriggeredAbility(
						new OnGainTrigger(
							new VictoryFilter()),
						new PlusVictoryTokens(1),
						TriggeredAbilityDurationType.WhileCardInPlay
						)),
				new PlusActions(1),
				new PlusCards(1)
			};
		}
	}
}