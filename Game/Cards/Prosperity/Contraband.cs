using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Prosperity
{
	public class Contraband : ICard
	{
		public Card Name { get; } = Card.Contraband;
		public int Cost { get; } = 5;
		public CardType CardType { get; } = CardType.Treasure;

	}
}