using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class OptionalAbility : IAbility, IResponseRequired<ActionResponse>
    {
        public IAbility Ability { get; set; }
        public string Message { get; set; }
        
        public OptionalAbility(IAbility ability, string message)
        {
            Ability = ability;
            Message = message;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            player.ActionRequest = new YesNoActionRequest(Card.None, Message);
            player.PlayStatus = PlayStatus.ActionRequestResponder;
        }

        public bool Resolved { get; set; }
        
        public void ResponseReceived(Game game, ActionResponse response)
        {
            var player = game.GetActivePlayer();
            

            if (response == ActionResponse.Yes)
            {
               player.RuleStack.Push(Ability);
               player.PlayStatus = PlayStatus.ActionPhase;
               Resolved = true;
            }

            if (response == ActionResponse.No)
            {
                player.PlayStatus = PlayStatus.ActionPhase;
                Resolved = true;
            }
        }
    }
}