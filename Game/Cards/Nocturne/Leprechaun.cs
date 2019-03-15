using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Nocturne
{
	public class Leprechaun : ICard
	{
		public Card Name { get; } = Card.Leprechaun;
		public int Cost { get; }
		public CardType CardType { get; }

	}
}