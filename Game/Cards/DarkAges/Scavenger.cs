using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.CardSpecificAbilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.DarkAges
{
	public class Scavenger : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.Scavenger;
		public int Cost { get; } = 4;
		public CardType CardType { get; } = CardType.Action;


		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
            return new List<IRule>
            {
                new ScavengerAbility(),
                new OptionalAbility(new DiscardDeck(), "Would you like to discard your deck?"),
                new PlusMoney(2)
            };
		}
	}
}