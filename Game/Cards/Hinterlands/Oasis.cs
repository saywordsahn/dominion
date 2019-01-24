using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Hinterlands
{
	public class Oasis : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Oasis;
		public int Cost { get; } = 3;
		public CardType CardType { get; } = CardType.Action;

		public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}


		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
            return new List<IRule>
            {
                new Discard(1),
                new PlusMoney(1),
                new PlusActions(1),
                new PlusCards(1)
            };
		}
	}
}