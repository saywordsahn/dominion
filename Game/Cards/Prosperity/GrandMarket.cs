using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Prosperity
{
	public class GrandMarket : ICard, IAction, IRulesHolder, IBuyConditionHolder
	{
		public Card Name { get; } = Card.GrandMarket;
		public int Cost { get; } = 6;
		public CardType CardType { get; } = CardType.Action;

		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new PlusMoney(2),
				new PlusBuys(1),
				new PlusActions(1),
				new PlusCards(1)
			};
		}

		public bool ResolveBuyCondition(Game game, IPlayer player)
		{
			if (player.PlayedCards.Any(x => x.Card.Name == Card.Copper))
			{
				return false;
			}

			return true;
		}
	}
}