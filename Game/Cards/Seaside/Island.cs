using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities.CardSpecificAbilities;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Seaside
{
    public class Island : IAction, ICard, IVictory, IRulesHolder
    {
        public Card Name { get; } = Card.Island;
        public int Cost { get; } = 4;
        public CardType CardType { get; } = CardType.Victory;
        public int GetVictoryPointValue(IPlayer player)
        {
            return 2;
        }

        public void Resolve(Game game)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            return new List<IRule> {new IslandAbility()};
        }
    }
}