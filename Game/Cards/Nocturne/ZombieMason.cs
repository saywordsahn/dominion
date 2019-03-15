using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Nocturne
{
	public class ZombieMason : ICard
	{
		public Card Name { get; } = Card.ZombieMason;
		public int Cost { get; }
		public CardType CardType { get; }

	}
}