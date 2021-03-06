using System.Collections.Generic;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Alchemy
{
	public class Transmute : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Transmute;
		public int Cost { get; } = 0;
		public CardType CardType { get; } = CardType.Action;


		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			throw new System.NotImplementedException();
		}
	}
}