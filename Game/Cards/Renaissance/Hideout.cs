using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.CardSpecificAbilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Renaissance
{
	public class Hideout : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Hideout;
		public int Cost { get; } = 4;
		public CardType CardType { get; } = CardType.Action;

		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new HideoutAbility(),
				new PlusActions(2),
				new PlusCards(1)
			};
		}
	}
}