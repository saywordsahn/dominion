using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Nocturne
{
	public class SecretCave : ICard
	{
		public Card Name { get; } = Card.SecretCave;
		public int Cost { get; }
		public CardType CardType { get; }

	}
}