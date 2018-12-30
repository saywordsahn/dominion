using System.Collections.Generic;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Guilds
{
	public class Masterpiece : ICard
	{
		public Card Name { get; } = Card.Masterpiece;
		public int Cost { get; } = 3;
		public CardType CardType { get; } = CardType.Treasure;

		public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}

	}
}