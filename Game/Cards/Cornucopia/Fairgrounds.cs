using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Cornucopia
{
	public class Fairgrounds : ICard
	{
		public Card Name { get; } = Card.Fairgrounds;
		public int Cost { get; } = 6;
		public CardType CardType { get; }

		public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}

	}
}