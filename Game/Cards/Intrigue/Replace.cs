using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Intrigue
{
	public class Replace : ICard
	{
		public Card Name { get; } = Card.Replace;
		public int Cost { get; } = 5;
		public CardType CardType { get; }

	}
}