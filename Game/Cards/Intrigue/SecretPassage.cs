using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Intrigue
{
	public class SecretPassage : ICard
	{
		public Card Name { get; } = Card.SecretPassage;
		public int Cost { get; } = 4;
		public CardType CardType { get; }

	}
}