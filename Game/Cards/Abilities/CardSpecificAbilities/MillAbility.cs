using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class MillAbility : IAbility, IResponseRequired<ActionResponse>
    {
        public void Resolve(Game game, IPlayer player)
        {
            if (player.Hand.Count < 2)
            {
                Resolved = true;
                return;
            }
            
            player.ActionRequest = new YesNoActionRequest(Card.None, "Discard 2 cards for +2 gold?");
            player.PlayStatus = PlayStatus.ActionRequestResponder;
        }
        
        public void ResponseReceived(Game game, ActionResponse response)
        {
            var player = game.GetActivePlayer();
            
            if (response == ActionResponse.Yes)
            {
                player.RuleStack.Push(new PlusMoney(2));
                player.RuleStack.Push(new Discard(2));
                player.PlayStatus = PlayStatus.ActionPhase;
                Resolved = true;
            }

            if (response == ActionResponse.No)
            {
                player.PlayStatus = PlayStatus.ActionPhase;
                Resolved = true;
            }
        }

        public bool Resolved { get; set; }
    }
}