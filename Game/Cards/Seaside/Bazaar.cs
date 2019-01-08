using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Seaside
{
	public class Bazaar : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Bazaar;
		public int Cost { get; } = 5;
		public CardType CardType { get; } = CardType.Action;

		public void Resolve(Game game)
		{
			throw new System.InvalidOperationException();
		}

		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new PlusMoney(1),
				new PlusActions(2),
				new PlusCards(1)
			};
		}
	}
}