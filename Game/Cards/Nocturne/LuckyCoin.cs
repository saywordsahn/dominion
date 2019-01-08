using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Nocturne
{
	public class LuckyCoin : ICard, ITreasure, IHeirloom, ITreasureAbilityHolder
	{
		public Card Name { get; } = Card.LuckyCoin;
		public int Cost { get; } = 4;
		public CardType CardType { get; } = CardType.Treasure;

		public int Value { get; } = 1;
		public void ResolveTreasureAbilities(IPlayer player)
		{
			player.RuleStack.Push(new GainCard(Card.Silver));
		}
	}
}