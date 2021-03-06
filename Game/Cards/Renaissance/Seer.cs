using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.CardSpecificAbilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Renaissance
{
	public class Seer : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Seer;
		public int Cost { get; } = 5;
		public CardType CardType { get; } = CardType.Action;

		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new SeerAbility(),
				new PlusActions(1),
				new PlusCards(1)
			};
		}
	}
}