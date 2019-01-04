using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Requests;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class MiserAbility : IAbility, IResponseRequired<IEnumerable<ActionResponse>>
    {
        public void Resolve(Game game, IPlayer player)
        {
            var options = new List<RequestOption>
            {
                new RequestOption(ActionResponse.Put, "Place a copper on tavern mat"),
                new RequestOption(ActionResponse.Gain, "+$1 per copper on tavern mat")
            };
            
            player.ActionRequest = new SelectOptionsActionRequest(
                "Select one:",
                Card.Miser, 
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
                    case ActionResponse.Put:
                        if (player.Hand.Contains(Card.Copper))
                        {
                            player.RuleStack.Push(new PutOnTavernMat(CardLocation.Hand, Card.Copper));
                        }
                        player.PlayStatus = PlayStatus.ActionPhase;
                        Resolved = true;
                        break;
                    case ActionResponse.Gain:
                        var copperCount = player.TavernMat.Cards.Count(x => x == Card.Copper);
                        player.RuleStack.Push(new PlusMoney(copperCount));
                        player.PlayStatus = PlayStatus.ActionPhase;
                        Resolved = true;
                        break;
                }
            }
        }
    }
}