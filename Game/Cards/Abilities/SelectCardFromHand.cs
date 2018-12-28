using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class SelectCardFromHand : IAbility
    {
        public bool Resolved { get; set; }
        public string Message { get; set; }

        public SelectCardFromHand(string message)
        {
            Message = message;
        }

        public void Resolve(Game game, IPlayer player)
        {
            player.ActionRequest = new SelectCardsActionRequest(Message,
                Card.Island, player.Hand, 1);
            player.PlayStatus = PlayStatus.ActionRequestResponder;
        }

    }
}