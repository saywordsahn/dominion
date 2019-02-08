using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities.CardSpecificAbilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Prosperity
{
	public class Expand : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Expand;
		public int Cost { get; } = 7;
		public CardType CardType { get; } = CardType.Action;

		public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}


		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
            return new List<IRule>{
                new RemodelAbility(3)
            };
		}
	}
}