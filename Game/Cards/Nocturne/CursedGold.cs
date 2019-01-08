using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities.Attacks.Effects;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Nocturne
{
	public class CursedGold : ICard, ITreasure, ITreasureAbilityHolder, IHeirloom
	{
		public Card Name { get; } = Card.CursedGold;
		public int Cost { get; } = 4;
		public CardType CardType { get; } = CardType.Treasure;

		public int Value { get; } = 3;
		
		public void ResolveTreasureAbilities(IPlayer player)
		{
			player.RuleStack.Push(new GainCurseAttackEffect());
		}
	}
}