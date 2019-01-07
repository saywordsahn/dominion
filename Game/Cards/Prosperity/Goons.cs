using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Prosperity
{
	public class Goons : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Goons;
		public int Cost { get; } = 6;
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