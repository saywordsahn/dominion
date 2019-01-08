using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.CardSpecificAbilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Empires
{
	public class BustlingVillage : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.BustlingVillage;
		public int Cost { get; } = 5;
		public CardType CardType { get; } = CardType.Action;

		public void Resolve(Game game)
		{
			var player = game.GetActivePlayer();

			foreach (var rule in GetRules(game, player))
			{
				player.RuleStack.Push(rule);
			}
		}

		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			return new List<IRule>
			{
				new SettlersBustlingVillageAbility(Card.Settlers),
				new PlusActions(3),
				new PlusCards(1)
			};
		}
	}
}