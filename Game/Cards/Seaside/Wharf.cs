using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;

namespace DominionWeb.Game.Cards.Seaside
{
    public class Wharf : ICard, IAction, IDuration
    {
        public Card Name { get; } = Card.Wharf;
        public int Cost { get; } = 5;
        public CardType CardType { get; } = CardType.Action;
        public bool Resolved { get; set; }
        
        public int NumberOfTurnsActive { get; set; }

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.Draw(2);
            player.NumberOfBuys++;
        }
        
        public void OnTurnStart(Game game)
        {
            var player = game.GetActivePlayer();
            player.MoneyPlayed++;
            Resolved = true;
        }
        
        public IEnumerable<IAbility> GetOnTurnStartAbilities(int numberOfTurnsActive)
        {
            Resolved = true;
            return new List<IAbility>() { new PlusCards(2), new PlusBuys(1) };
        }

    }
}