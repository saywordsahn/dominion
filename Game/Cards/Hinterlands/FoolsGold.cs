using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Hinterlands
{
	public class FoolsGold : ICard
	{
		public Card Name { get; } = Card.FoolsGold;
		public int Cost { get; } = 2;
		public CardType CardType { get; } = CardType.Treasure;

	}
}