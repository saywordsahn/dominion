using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Prosperity
{
    public class Monument : ICard, IAction, IRulesHolder
    {
        public int Cost { get; } = 4;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Monument;

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.MoneyPlayed += 2;
            player.VictoryTokens++;
        }

        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            return new List<IRule>
            {
                new PlusVictoryTokens(1),
                new PlusMoney(2)
            };
        }
    }
}