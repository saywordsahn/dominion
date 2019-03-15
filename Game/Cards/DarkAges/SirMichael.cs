using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.DarkAges
{
	public class SirMichael : ICard
	{
		public Card Name { get; } = Card.SirMichael;
		public int Cost { get; } = 5;
		public CardType CardType { get; }

	}
}