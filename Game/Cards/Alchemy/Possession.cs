using System.Collections.Generic;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Alchemy
{
	public class Possession : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Possession;
		public int Cost { get; } = 6;
		public CardType CardType { get; } = CardType.Action;

		public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}


		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			throw new System.NotImplementedException();
		}
	}
}