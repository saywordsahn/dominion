using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Renaissance
{
	public class Spices : ICard, ITreasure, ITreasureAbilityHolder, IOnGainAbilityHolder
	{
		public Card Name { get; } = Card.Spices;
		public int Cost { get; } = 5;
		public CardType CardType { get; } = CardType.Treasure;

		public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}

		public int Value { get; } = 2;
		
		public void ResolveTreasureAbilities(IPlayer player)
		{
			player.RuleStack.Push(new PlusBuys(1));
		}
		
		public void ResolveOnGainAbilities(IPlayer player)
		{
			player.RuleStack.Push(new PlusCoffers(2));
		}
	}
}