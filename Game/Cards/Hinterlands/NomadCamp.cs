using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Hinterlands
{
    public class NomadCamp : ICard, IAction, IOnGainOverride
    {
        public Card Name { get; } = Card.NomadCamp;
        public int Cost { get; } = 4;
        public CardType CardType { get; } = CardType.Action;

        public void OnGain(IPlayer player, Card card)
        {
            player.Deck.Add(card);
        }

        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            return new List<IRule>
            {
                new PlusBuys(1),
                new PlusMoney(2)
            };
        }
    }
}