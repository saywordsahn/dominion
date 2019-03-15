using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.Attacks;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.DarkAges
{
	public class Marauder : ICard, IAction, IAttack, IRulesHolder, ILooter
	{
		public Card Name { get; } = Card.Marauder;
		public int Cost { get; } = 4;
		public CardType CardType { get; } = CardType.Action;

		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new Attack(new GainCard(Card.VirtualRuins)),
				new GainCard(Card.Spoils, 1)
			};
		}
	}
}