using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Intrigue
{
	public class Ironworks : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Ironworks;
		public int Cost { get; } = 4;
		public CardType CardType { get; } = CardType.Action;

		public void Resolve(Game game)
		{
			
		}


		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			throw new System.NotImplementedException();
		}
	}
}