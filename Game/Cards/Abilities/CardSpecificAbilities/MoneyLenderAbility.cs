using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class MoneyLenderAbility : IAbility, IResponseRequired<ActionResponse>
    {
        public bool Resolved { get; set; }

        public void Resolve(Game game, IPlayer player)
        {
            if (player.Hand.Contains(Card.Copper))
            {
                player.ActionRequest = new YesNoActionRequest(Card.Moneylender, "Would you like to trash a copper to gain $3?");
                player.PlayStatus = PlayStatus.ActionRequestResponder;
            }
        }

        public void ResponseReceived(Game game, ActionResponse response)
        {
            var player = game.GetActivePlayer();

            if (response == ActionResponse.Yes)
            {
                var copper = player.Hand.Find(x => x == Card.Copper);
                player.TrashFromHand(game.Supply, copper);
                player.MoneyPlayed += 3;
            }

            player.PlayStatus = PlayStatus.ActionPhase;
            Resolved = true;
        }
    }
}
