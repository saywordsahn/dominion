using System.Collections.Generic;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Intrigue
{
	public class Harem : ICard
	{
		public Card Name { get; } = Card.Harem;
		public int Cost { get; } = 6;
		public CardType CardType { get; } = CardType.Treasure;

		public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}

	}
}