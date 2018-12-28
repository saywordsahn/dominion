using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.CardSpecificAbilities;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Intrigue
{
    public class Lurker : ICard, IAction, IRulesHolder
    {
        public Card Name { get; } = Card.Lurker;
        public int Cost { get; } = 2;
        public CardType CardType { get; } = CardType.Action;
        public void Resolve(Game game)
        {
            //placeholder until all cards are converted
            throw new System.NotImplementedException();
        }

        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            return new List<IRule> {new LurkerAbility(), new PlusActions(1)};
        }
    }
}