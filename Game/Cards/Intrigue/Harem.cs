using System.Collections.Generic;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Intrigue
{
	public class Harem : ICard, ITreasure, IVictory
	{
		public Card Name { get; } = Card.Harem;
		public int Cost { get; } = 6;
		public CardType CardType { get; } = CardType.Treasure;

		public int Value { get; } = 2;
		public int GetVictoryPointValue(IPlayer player)
		{
			return 2;
		}
	}
}