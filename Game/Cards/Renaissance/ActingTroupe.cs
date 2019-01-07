using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Renaissance
{
    public class ActingTroupe : ICard, IAction, IRulesHolder
    {
        public Card Name { get; } = Card.ActingTroupe;
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
                new TrashCard(CardLocation.PlayedCards, Card.ActingTroupe),
                new PlusVillagers(4)
            };
        }
    }
}