using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Intrigue
{
	public class Diplomat : ICard
	{
		public Card Name { get; } = Card.Diplomat;
		public int Cost { get; } = 4;
		public CardType CardType { get; }

	}
}