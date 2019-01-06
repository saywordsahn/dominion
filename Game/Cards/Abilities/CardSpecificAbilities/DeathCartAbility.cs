using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Requests;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class DeathCartAbility : IAbility, IResponseRequired<IEnumerable<ActionResponse>>
    {
        public void Resolve(Game game, IPlayer player)
        {
            if (!player.HasActionInHand())
            {
                player.RuleStack.Push(new TrashCard(CardLocation.PlayedCards, Card.DeathCart) );
                player.PlayStatus = PlayStatus.ActionPhase;
                Resolved = true;
                return;
            }
            
            var options = new List<RequestOption>
            {
                new RequestOption(ActionResponse.TrashActionCard, "Trash an action card?"),
                new RequestOption(ActionResponse.TrashThis, "Trash this")
            };
            
            player.ActionRequest = new SelectOptionsActionRequest(
                "Select one:",
                Card.DeathCart, 
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
                    case ActionResponse.TrashActionCard:
                        player.RuleStack.Push(new TrashFromHand(new ActionFilter()));
                        player.PlayStatus = PlayStatus.ActionPhase;
                        Resolved = true;
                        break;
                    case ActionResponse.TrashThis:
                        player.RuleStack.Push(new TrashCard(CardLocation.PlayedCards, Card.DeathCart));
                        player.PlayStatus = PlayStatus.ActionPhase;
                        Resolved = true;
                        break;
                }
            }
        }
    }
}