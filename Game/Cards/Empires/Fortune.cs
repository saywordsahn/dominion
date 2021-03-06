using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Empires
{
	public class Fortune : ICard
	{
		public Card Name { get; } = Card.Fortune;
		public int Cost { get; }
		public CardType CardType { get; }

	}
}