using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;
using DominionWeb.Game.Utils;

namespace DominionWeb.Game.Cards.Hinterlands
{
	public class Inn : ICard, IAction, IRulesHolder, IOnGainAbilityHolder
	{
		public Card Name { get; } = Card.Inn;
		public int Cost { get; } = 5;
		public CardType CardType { get; } = CardType.Action;

		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
            return new List<IRule>
            {
                new Discard(2),
                new PlusActions(2),
                new PlusCards(2)
            };
		}

        public void ResolveOnGainAbilities(IPlayer player)
        {
            var filter = new ActionFilter();
            var actionCards = player.DiscardPile
                                .Select(CardFactory.Create)
                                .Where(filter.Apply).ToList();

            player.DiscardPile.RemoveAll(x => filter.Apply(CardFactory.Create(x)) == true);

            //TODO: reveal

            player.Deck.AddRange(actionCards.Select(x => x.Name));
            player.Deck.Shuffle();
        }
    }
}