using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.AttackEffects;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Renaissance
{
	public class Scholar : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Scholar;
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
				new PlusCards(7),
				new DiscardHand()
			};
		}
	}
}