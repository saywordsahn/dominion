using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities.CardSpecificAbilities;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Adventures
{
	public class Miser : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Miser;
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
				new MiserAbility()
			};
		}
	}
}