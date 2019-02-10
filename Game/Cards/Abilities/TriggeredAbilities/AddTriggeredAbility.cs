using System;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.TriggeredAbilities
{
    public class AddTriggeredAbility : IAbility
    {
        ITriggeredAbility Ability { get; set; }
        public bool Resolved { get; set; }

        public AddTriggeredAbility(ITriggeredAbility ability)
        {
            Ability = ability;
        }

        public void Resolve(Game game, IPlayer player)
        {
            player.TriggeredAbilities.Add(Ability);
            Resolved = true;
        }
    }
}
