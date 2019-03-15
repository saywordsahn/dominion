using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.DarkAges
{
	public class Feodum : ICard
	{
		public Card Name { get; } = Card.Feodum;
		public int Cost { get; } = 4;
		public CardType CardType { get; }

	}
}