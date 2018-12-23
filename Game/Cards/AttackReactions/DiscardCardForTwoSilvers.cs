
using System.Linq;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.AttackReactions
{
    public class DiscardCardForTwoSilvers : IRule, IResponseRequired<ActionResponse>
    {
        public bool Resolved { get; set; }
        
        public void Resolve(Game game, IPlayer player)
        {
            player.ActionRequest = new YesNoActionRequest(Card.Beggar, "Discard Beggar to gain two silvers?");
            player.PlayStatus = PlayStatus.ActionRequestResponder;
        }

        public void ResponseReceived(Game game, ActionResponse response)
        {
            //TODO: code unhappy path
            //TODO: check rule and perhaps code GainToDeck
            var player = game.GetActivePlayer();

            if (response == ActionResponse.Yes)
            {
                player.Hand.Remove(Card.Beggar);
                player.DiscardPile.Add(Card.Beggar);
                //TODO: add to played reactions? (with new interface to determine which reactions get added back to hand)
                player.Gain(Card.Silver);
                player.Gain(Card.Silver);
                game.Supply.Take(Card.Silver);
                game.Supply.Take(Card.Silver);
                var lastSilver = player.DiscardPile.Last();
                player.DiscardPile.RemoveAt(player.DiscardPile.Count - 1);
                player.Deck.Add(lastSilver);
                
            }

            Resolved = true;
            player.PlayStatus = PlayStatus.AttackResponder;
        }
    }
}