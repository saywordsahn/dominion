using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Renaissance
{
	public class Research : ICard
	{
		public Card Name { get; } = Card.Research;
		public int Cost { get; }
		public CardType CardType { get; }

	}
}