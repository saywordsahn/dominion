using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.CardSpecificAbilities;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Requests;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Intrigue
{
	public class Steward : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Steward;
		public int Cost { get; } = 3;
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
					new RequestOptionAbility(new RequestOption(ActionResponse.Draw, "+2 cards"), new PlusCards(2)),
					new RequestOptionAbility(new RequestOption(ActionResponse.Money, "+$2"), new PlusMoney(2)),
					new RequestOptionAbility(new RequestOption(ActionResponse.Trash, "Trash 2 cards"), new TrashFromHand(new NoFilter(), 2)),
				}, 1)				
			};
		}
	}
}