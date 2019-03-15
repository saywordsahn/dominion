using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Renaissance
{
	public class MountainVillage : ICard
	{
		public Card Name { get; } = Card.MountainVillage;
		public int Cost { get; }
		public CardType CardType { get; }

	}
}