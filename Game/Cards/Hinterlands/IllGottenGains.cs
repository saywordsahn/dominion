using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Hinterlands
{
	public class IllGottenGains : ICard
	{
		public Card Name { get; } = Card.IllGottenGains;
		public int Cost { get; } = 5;
		public CardType CardType { get; } = CardType.Treasure;

	}
}