using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Requests;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class SelectAbilities : IAbility, IResponseRequired<IEnumerable<ActionResponse>>
    {
        public IList<RequestOptionAbility> Options { get; set; }
        public int AmountToSelect { get; set; }
        
        public SelectAbilities(IList<RequestOptionAbility> options, int amountToSelect)
        {
            Options = options;
            AmountToSelect = amountToSelect;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            var options = Options.Select(x => x.RequestOption).ToList();
            
            player.ActionRequest = new SelectOptionsActionRequest(
                "Select " + AmountToSelect + ":",
                Card.Pawn, 
                options, 
                AmountToSelect);
            
            player.PlayStatus = PlayStatus.ActionRequestResponder;
        }

        public bool Resolved { get; set; }
        
        public void ResponseReceived(Game game, IEnumerable<ActionResponse> responses)
        {
            var player = game.GetActivePlayer();

            var responseList = responses.ToList();

            //ensure responses are different
            if (responseList.Count == AmountToSelect)
            {
                foreach (var response in responseList)
                {
                    player.RuleStack.Push(Options.Single(x => x.RequestOption.ActionResponse == response).Ability);
                }
                
                player.PlayStatus = PlayStatus.ActionPhase;
                Resolved = true;
            }
        }
    }
}