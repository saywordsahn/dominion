using System.Collections.Generic;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Alchemy
{
	public class Vineyard : ICard
	{
		public Card Name { get; } = Card.Vineyard;
		public int Cost { get; } = 1;
		public CardType CardType { get; }

		public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}

	}
}