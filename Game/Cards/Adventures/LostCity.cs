using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Adventures
{
	public class LostCity : ICard, IAction, IRulesHolder, IOnGainAbilityHolder
	{
		public Card Name { get; } = Card.LostCity;
		public int Cost { get; } = 5;
		public CardType CardType { get; } = CardType.Action;

		public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new PlusCards(2),
				new PlusActions(2)
			};
		}

		public void ResolveOnGainAbilities(IPlayer player)
		{
			player.RuleStack.Push(new OtherPlayersGainCard());
		}
	}
}