using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Renaissance
{
	public class Recruiter : ICard, IAction
	{
		public Card Name { get; } = Card.Recruiter;
		public int Cost { get; }
		public CardType CardType { get; }

        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            throw new System.NotImplementedException();
        }

        public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}

	}
}