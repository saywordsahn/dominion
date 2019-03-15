using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.DarkAges
{
	public class SirVander : ICard
	{
		public Card Name { get; } = Card.SirVander;
		public int Cost { get; } = 5;
		public CardType CardType { get; }

	}
}