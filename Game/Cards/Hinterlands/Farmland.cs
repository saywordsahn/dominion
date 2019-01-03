using System.Collections.Generic;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Hinterlands
{
	public class Farmland : ICard
	{
		public Card Name { get; } = Card.Farmland;
		public int Cost { get; } = 6;
		public CardType CardType { get; }

		public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}

	}
}