using DominionWeb.Game.Cards.Abilities.TriggeredAbilities;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class AddTriggeredAbility : IAbility
    {
        public ITriggeredAbility TriggeredAbility { get; set; }

        public AddTriggeredAbility(ITriggeredAbility triggeredAbility)
        {
            TriggeredAbility = triggeredAbility;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            player.TriggeredAbilities.Add(TriggeredAbility);
            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}