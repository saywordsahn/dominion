using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Prosperity
{
	public class Quarry : ICard
	{
		public Card Name { get; } = Card.Quarry;
		public int Cost { get; } = 4;
		public CardType CardType { get; } = CardType.Treasure;

	}
}