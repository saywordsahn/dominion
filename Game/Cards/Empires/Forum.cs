using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Empires
{
	public class Forum : ICard, IAction, IRulesHolder, IOnBuyAbilityHolder
	{
		public Card Name { get; } = Card.Forum;
		public int Cost { get; } = 5;
		public CardType CardType { get; } = CardType.Action;

		public void Resolve(Game game)
		{
			
		}

		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new Discard(2),
				new PlusActions(1),
				new PlusCards(3)
			};
		}

		public void ResolveOnGainAbilities(IPlayer player)
		{
			player.RuleStack.Push(new PlusBuys(1));
		}
	}
}