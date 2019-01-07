using System.Collections.Generic;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Hinterlands
{
	public class SilkRoad : ICard, IVictory
	{
		public Card Name { get; } = Card.SilkRoad;
		public int Cost { get; } = 4;
		public CardType CardType { get; } = CardType.Victory;

		public int GetVictoryPointValue(IPlayer player)
		{
			var victoryCardCount = player.GetCardCount(new VictoryFilter());

			return victoryCardCount / 4;
		}
	}
}