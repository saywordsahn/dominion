using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Seaside
{
	public class Treasury : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Treasury;
		public int Cost { get; } = 5;
		public CardType CardType { get; } = CardType.Action;


		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			throw new System.NotImplementedException();
		}
	}
}