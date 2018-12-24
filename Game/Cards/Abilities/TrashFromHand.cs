using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    //TODO: refactor this to be modifiable on card and number of cards
    public class TrashFromHand : IAbility, IResponseRequired<ActionResponse>
    {
        public PlayStatus PriorStatus { get; set; }
        
        // ReSharper disable once MemberCanBePrivate.Global
        // Json Serializer needs access to set for serialization
        public bool Resolved { get; set; }
        
        public void Resolve(Game game, IPlayer player)
        {
            if (player.HasCardInHand(Card.Copper))
            {
                PriorStatus = player.PlayStatus;
                player.ActionRequest = new YesNoActionRequest(Card.Vassal, "Would you like to trash a copper?");
                player.PlayStatus = PlayStatus.ActionRequestResponder;
            }
            else
            {
                Resolved = true;
            }
        }

        public void ResponseReceived(Game game, ActionResponse response)
        {
            var player = game.GetActivePlayer();

            if (response == ActionResponse.Yes)
            {
                player.Hand.Remove(Card.Copper);
                game.Supply.AddToTrash(Card.Copper);
            }

            player.PlayStatus = PriorStatus;
            Resolved = true;
        }

    }
}