using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Base
{
    public class Laboratory : ICard, IAction, IRulesHolder
    {
        public int Cost { get; } = 5;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Laboratory;

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.Draw(2);
            player.NumberOfActions++;
        }


        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            return new List<IRule>
            {
                new PlusActions(1),
                new PlusCards(2)
            };
        }
    }
}