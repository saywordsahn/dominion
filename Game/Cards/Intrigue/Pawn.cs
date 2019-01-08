using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Requests;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;
using Microsoft.Extensions.Options;

namespace DominionWeb.Game.Cards.Intrigue
{
	public class Pawn : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Pawn;
		public int Cost { get; } = 2;
		public CardType CardType { get; } = CardType.Action;

		public void Resolve(Game game)
		{
			
		}


		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new SelectAbilities(new List<RequestOptionAbility>
				{
					new RequestOptionAbility(new RequestOption(ActionResponse.Draw, "+1 card"), new PlusCards(1)),
					new RequestOptionAbility(new RequestOption(ActionResponse.Action, "+1 action"), new PlusActions(1)),
					new RequestOptionAbility(new RequestOption(ActionResponse.Buy, "+1 buy"), new PlusBuys(1)),
					new RequestOptionAbility(new RequestOption(ActionResponse.Money, "+$1"), new PlusMoney(1)),
				}, 2)
			};
		}
	}
}