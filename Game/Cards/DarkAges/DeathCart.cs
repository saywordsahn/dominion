using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.CardSpecificAbilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.DarkAges
{
	public class DeathCart : ICard, IAction, IRulesHolder, IOnGainAbilityHolder, ILooter
	{
		public Card Name { get; } = Card.DeathCart;
		public int Cost { get; } = 4;
		public CardType CardType { get; } = CardType.Action;

		public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}


		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new DeathCartAbility(),
				new PlusMoney(5)
			};
		}

		public void ResolveOnGainAbilities(IPlayer player)
		{
			player.RuleStack.Push(new GainCard(Card.VirtualRuins));
			player.RuleStack.Push(new GainCard(Card.VirtualRuins));
		}
	}
}