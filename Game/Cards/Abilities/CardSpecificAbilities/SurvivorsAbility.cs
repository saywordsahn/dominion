using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Requests;
using DominionWeb.Game.Player;
using DominionWeb.Game.Utils;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class SurvivorsAbility : IAbility, IResponseRequired<IEnumerable<ActionResponse>>
    {
        public void Resolve(Game game, IPlayer player)
        {
            var topCards = player.GetTopCards(2);

            player.GameLog.Add(player.PlayerName.Substring(0,1) + " draws " + string.Join(", ", topCards));
            
            var options = new List<RequestOption>
            {
                new RequestOption(ActionResponse.Discard, "Discard cards"),
                new RequestOption(ActionResponse.Put, "Place on deck")
            };
            
            player.ActionRequest = new SelectOptionsActionRequest(
                "Select one:",
                Card.Survivors, 
                options, 
                1);
            
            player.PlayStatus = PlayStatus.ActionRequestResponder;
        }

        public bool Resolved { get; set; }
        
        public void ResponseReceived(Game game, IEnumerable<ActionResponse> responses)
        {
            var responseList = responses.ToList();
            var player = game.GetActivePlayer();

            if (responseList.Count != 1) return;

            switch (responseList[0])
            {
                case ActionResponse.Discard:
                    var cards = player.Deck.TakeLast(2);
                    player.DiscardPile.AddRange(cards);
                    player.Deck.RemoveFrom(player.Deck.Count - 2);
                    //TODO: check for discard triggers
                    //TODO: implement as generic ability
                    player.PlayStatus = PlayStatus.ActionPhase;
                    Resolved = true;
                    break;
                case ActionResponse.Put:
                    player.RuleStack.Push(new OrderTopOfDeck(2));
                    player.PlayStatus = PlayStatus.ActionPhase;
                    Resolved = true;
                    break;

            }
        }
    }
}