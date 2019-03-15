using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.DarkAges
{
	public class Altar : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Altar;
		public int Cost { get; } = 6;
		public CardType CardType { get; } = CardType.Action;


		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
            return new List<IRule>
            {
                new GainCardCostingUpToX(5, GainTarget.DiscardPile),
                new TrashFromHand(new NoFilter())
            };
		}
	}
}