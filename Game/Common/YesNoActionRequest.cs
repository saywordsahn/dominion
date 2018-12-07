using DominionWeb.Game.Cards;

namespace DominionWeb.Game.Common
{
    public class YesNoActionRequest : IActionRequest
    {
        public ActionRequestType ActionRequestType { get; } = ActionRequestType.YesNo;
        public string Message { get; private set; }
        public Card Requester { get; private set; }

        public YesNoActionRequest(Card requester, string message)
        {
            Requester = requester;
            Message = message;
        }
    }
}