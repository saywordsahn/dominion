using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.DarkAges
{
	public class DameNatalie : ICard
	{
		public Card Name { get; } = Card.DameNatalie;
		public int Cost { get; } = 5;
		public CardType CardType { get; }

		public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}

	}
}