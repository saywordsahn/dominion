using System;
using System.Linq;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Common;

namespace DominionWeb.Game.Cards.Base
{
    public class Moneylender : ICard, IAction, IResponseRequired<ActionResponse>
    {
        public int Cost { get; } = 4;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Moneylender;

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();

            if (player.Hand.Contains(Card.Copper))
            {
                player.ActionRequest = new YesNoActionRequest(Card.Moneylender, "Would you like to trash a copper to gain $3?");
                player.PlayStatus = PlayStatus.ActionRequestResponder;
            }
        }

        public void ResponseReceived(Game game, ActionResponse response)
        {
            var player = game.GetActivePlayer();
            player.PlayStatus = PlayStatus.ActionPhase;

            if (response == ActionResponse.Yes)
            {
                var copper = player.Hand.Find(x => x == Card.Copper);
                player.TrashFromHand(game.Supply, copper);
                player.MoneyPlayed += 3;
            }

        }
    }
}