using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Adventures
{
	public class Ratcatcher : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Ratcatcher;
		public int Cost { get; } = 2;
		public CardType CardType { get; } = CardType.Action;

		public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new PutOnTavernMat(CardLocation.PlayedCards, Card.Ratcatcher),
				new PlusActions(1),
				new PlusCards(1)
			};
		}
	}
}