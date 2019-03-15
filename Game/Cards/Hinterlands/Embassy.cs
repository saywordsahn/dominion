using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Hinterlands
{
	public class Embassy : ICard, IAction, IRulesHolder, IOnGainAbilityHolder
	{
		public Card Name { get; } = Card.Embassy;
		public int Cost { get; } = 5;
		public CardType CardType { get; } = CardType.Action;

		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new Discard(3),
				new PlusCards(5)
			};
		}

		public void ResolveOnGainAbilities(IPlayer player)
		{
			player.RuleStack.Push(new OtherPlayersGainCard(Card.Silver));
		}
	}
}