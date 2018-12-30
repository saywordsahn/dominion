using System.Collections.Generic;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Hinterlands
{
	public class SilkRoad : ICard
	{
		public Card Name { get; } = Card.SilkRoad;
		public int Cost { get; } = 4;
		public CardType CardType { get; }

		public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}

	}
}