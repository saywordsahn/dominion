using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities.CardSpecificAbilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Prosperity
{
	public class CountingHouse : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.CountingHouse;
		public int Cost { get; } = 5;
		public CardType CardType { get; } = CardType.Action;

		public void Resolve(Game game)
		{
		}


		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new CountingHouseAbility()
			};
		}
	}
}