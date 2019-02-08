using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.CardSpecificAbilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.DarkAges
{
	public class Armory : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Armory;
		public int Cost { get; } = 4;
		public CardType CardType { get; } = CardType.Action;

		public void Resolve(Game game)
		{
		}

		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
            return new List<IRule>
            {
                new GainCardCostingUpToX(4, GainTarget.Deck)
			};
		}
	}
}