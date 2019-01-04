using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.CardSpecificAbilities;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Adventures
{
	public class Ranger : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Ranger;
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
				new RangerAbility(),
				new FlipJourneyToken(),
				new PlusBuys(1)
			};
		}
	}
}