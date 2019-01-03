using System.Collections.Generic;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Renaissance
{
	public class BorderGuard : ICard
	{
		public Card Name { get; } = Card.BorderGuard;
		public int Cost { get; }
		public CardType CardType { get; }

		public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}

	}
}