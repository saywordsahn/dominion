using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.Attacks;
using DominionWeb.Game.Cards.Abilities.TriggeredAbilities;
using DominionWeb.Game.Cards.Base;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;
using DominionWeb.Game.Player.Triggers;

namespace DominionWeb.Game.Cards.Prosperity
{
	public class Goons : ICard, IAction, IAttack, IRulesHolder
	{
		public Card Name { get; } = Card.Goons;
		public int Cost { get; } = 6;
		public CardType CardType { get; } = CardType.Action;


		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new AddTriggeredAbility(
					new TriggeredAbility(
						new OnBuyTrigger(
							new NoFilter()), 
						new PlusVictoryTokens(1),
						TriggeredAbilityDurationType.WhileCardInPlay
					)),
				new MilitiaAttack(),
				new PlusMoney(2),
				new PlusBuys(1)
			};
		}
	}
}