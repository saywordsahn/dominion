using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;

namespace DominionWeb.Game.Cards
{
    public interface IDuration
    {
        int NumberOfTurnsActive { get; set; }
        bool Resolved { get; set; }
        IEnumerable<IAbility> GetOnTurnStartAbilities(int numberOfTurnsActive);
    }
}