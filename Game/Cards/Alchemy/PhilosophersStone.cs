using System.Collections.Generic;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Alchemy
{
	public class PhilosophersStone : ICard
	{
		public Card Name { get; } = Card.PhilosophersStone;
		public int Cost { get; } = 3;
		public CardType CardType { get; } = CardType.Treasure;

		public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}

	}
}