using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Nocturne
{
	public class ZombieSpy : ICard
	{
		public Card Name { get; } = Card.ZombieSpy;
		public int Cost { get; }
		public CardType CardType { get; }

	}
}