using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.CardSpecificAbilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Empires
{
	public class Settlers : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Settlers;
		public int Cost { get; } = 2;
		public CardType CardType { get; } = CardType.Action;

		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new SettlersBustlingVillageAbility(Card.Copper),
				new PlusActions(1),
				new PlusCards(1)
			};
		}
	}
}