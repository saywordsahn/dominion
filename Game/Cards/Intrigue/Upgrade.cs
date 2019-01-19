using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Intrigue
{
	public class Upgrade : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Upgrade;
		public int Cost { get; } = 5;
		public CardType CardType { get; } = CardType.Action;

		public void Resolve(Game game)
		{
		}


		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				//TODO implement upgrade action
				new PlusActions(1),
				new PlusCards(1)
			};
		}
	}
}