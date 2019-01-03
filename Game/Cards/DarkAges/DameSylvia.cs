using System.Collections.Generic;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.DarkAges
{
	public class DameSylvia : ICard
	{
		public Card Name { get; } = Card.DameSylvia;
		public int Cost { get; } = 5;
		public CardType CardType { get; }

		public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}

	}
}