using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Nocturne
{
	public class Pasture : ICard, ITreasure, IVictory, IHeirloom
	{
		public Card Name { get; } = Card.Pasture;
		public int Cost { get; } = 2;
		public CardType CardType { get; } = CardType.Treasure;

		public int Value { get; } = 1;
		public int GetVictoryPointValue(IPlayer player)
		{
			throw new System.NotImplementedException();
		}
	}
}