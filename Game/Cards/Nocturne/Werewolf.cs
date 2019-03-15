using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Nocturne
{
	public class Werewolf : ICard
	{
		public Card Name { get; } = Card.Werewolf;
		public int Cost { get; }
		public CardType CardType { get; }

	}
}