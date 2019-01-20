using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.CardSpecificAbilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;
using Microsoft.AspNetCore.Rewrite.Internal.IISUrlRewrite;

namespace DominionWeb.Game.Cards.Intrigue
{
	public class Mill : ICard, IAction, IRulesHolder, IVictory
	{
		public Card Name { get; } = Card.Mill;
		public int Cost { get; } = 4;
		public CardType CardType { get; } = CardType.Action;

		public void Resolve(Game game)
		{
		}

		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new MillAbility(),
				new PlusActions(1),
				new PlusCards(1)
			};
		}

		public int GetVictoryPointValue(IPlayer player)
		{
			return 1;
		}
	}
}