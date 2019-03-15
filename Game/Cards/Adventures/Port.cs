using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Adventures
{
	public class Port : ICard, IAction, IRulesHolder, IOnBuyAbilityHolder
	{
		public Card Name { get; } = Card.Port;
		public int Cost { get; } = 4;
		public CardType CardType { get; } = CardType.Action;


		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new PlusActions(2),
				new PlusCards(1)
			};
		}

		public void ResolveOnGainAbilities(IPlayer player)
		{
			player.RuleStack.Push(new GainCard(Card.Port));
		}
	}
}