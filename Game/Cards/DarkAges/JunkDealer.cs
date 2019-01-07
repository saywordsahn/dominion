using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.DarkAges
{
    
    public class JunkDealer : ICard, IAction, IRulesHolder
    {
        public Card Name { get; } = Card.JunkDealer;
        public int Cost { get; } = 5;
        public CardType CardType { get; } = CardType.Action;
        public void Resolve(Game game)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            return new List<IRule>
            {        
                new TrashFromHand(new NoFilter()),
                new PlusMoney(1),
                new PlusActions(1),
                new PlusCards(1)
            };
        }
    }
}