using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;

namespace DominionWeb.Game.Cards.Seaside
{
    public class Lighthouse : ICard, IAction, IDuration
    {
        public Card Name { get; } = Card.Lighthouse;
        public int Cost { get; } = 2;
        public CardType CardType { get; } = CardType.Action;
        public bool Resolved { get; set; }
        
        public int NumberOfTurnsActive { get; set; }

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.NumberOfActions++;
            player.MoneyPlayed++;
        }
        
        public IEnumerable<IAbility> GetOnTurnStartAbilities(int numberOfTurnsActive)
        {
            Resolved = true;
            return new List<IAbility>() { new PlusMoney(1) };
        }

    }
}