using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Empires
{
	public class Enchantress : ICard
	{
		public Card Name { get; } = Card.Enchantress;
		public int Cost { get; }
		public CardType CardType { get; }

	}
}