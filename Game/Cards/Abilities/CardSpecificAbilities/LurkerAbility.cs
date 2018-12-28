using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Requests;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;
using Microsoft.AspNet.SignalR.Hosting;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class LurkerAbility : IAbility, IResponseRequired<IEnumerable<ActionResponse>>
    {
        public bool Resolved { get; set; }

        public void Resolve(Game game, IPlayer player)
        {

            var options = new List<RequestOption>
            {
                new RequestOption(ActionResponse.Trash, "Trash an action card from the Supply"),
                new RequestOption(ActionResponse.Gain, "Gain an action card from the trash")
            };
            
            player.ActionRequest = new SelectOptionsActionRequest(
                "Select one:",
                Card.Lurker, 
                options, 
                1);
            
            player.PlayStatus = PlayStatus.ActionRequestResponder;
        }
        
        public void ResponseReceived(Game game, IEnumerable<ActionResponse> responses)
        {
            var player = game.GetActivePlayer();

            var responseList = responses.ToList();

            if (responseList.Count == 1)
            {
                var response = responseList[0];
                
                switch (response)
                {
                    case ActionResponse.Trash:
                        player.RuleStack.Push(new TrashFromSupply(new ActionFilter()));
                        Resolved = true;
                        break;
                    case ActionResponse.Gain:
                        player.RuleStack.Push(new GainFromTrash(new ActionFilter()));
                        Resolved = true;
                        break;
                }
            }
           
        }
    }
}