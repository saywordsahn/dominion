using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Empires
{
	public class Encampment : ICard
	{
		public Card Name { get; } = Card.Encampment;
		public int Cost { get; }
		public CardType CardType { get; }

	}
}