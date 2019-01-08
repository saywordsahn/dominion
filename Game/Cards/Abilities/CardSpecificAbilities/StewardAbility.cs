using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Requests;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class StewardAbility : IAbility, IResponseRequired<IEnumerable<ActionResponse>>
    {
        public void Resolve(Game game, IPlayer player)
        {
            var options = new List<RequestOption>
            {
                new RequestOption(ActionResponse.Draw, "+2 cards"),
                new RequestOption(ActionResponse.Money, "+$2"),
                new RequestOption(ActionResponse.Trash, "Trash 2 cards")
            };
            
            player.ActionRequest = new SelectOptionsActionRequest(
                "Select one:",
                Card.Steward, 
                options, 
                1);
            
            player.PlayStatus = PlayStatus.ActionRequestResponder;
        }

        public bool Resolved { get; set; }
        public void ResponseReceived(Game game, IEnumerable<ActionResponse> responses)
        {
            var player = game.GetActivePlayer();

            var responseList = responses.ToList();

            if (responseList.Count == 1)
            {
                var response = responseList[0];
                
                switch (response)
                {
                    case ActionResponse.Draw:
                        player.RuleStack.Push(new PlusCards(2));
                        player.PlayStatus = PlayStatus.ActionPhase;
                        Resolved = true;
                        break;
                    case ActionResponse.Money:
                        player.RuleStack.Push(new PlusMoney(2));
                        player.PlayStatus = PlayStatus.ActionPhase;
                        Resolved = true;
                        break;
                    case ActionResponse.Trash:
                        player.RuleStack.Push(new TrashFromHand(new NoFilter(), 2));
                        player.PlayStatus = PlayStatus.ActionPhase;
                        Resolved = true;
                        break;
                }
            }
        }
    }
}