using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Intrigue
{
	public class Patrol : ICard
	{
		public Card Name { get; } = Card.Patrol;
		public int Cost { get; } = 5;
		public CardType CardType { get; }

	}
}