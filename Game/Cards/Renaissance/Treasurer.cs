using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Renaissance
{
	public class Treasurer : ICard
	{
		public Card Name { get; } = Card.Treasurer;
		public int Cost { get; }
		public CardType CardType { get; }

	}
}