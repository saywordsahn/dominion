namespace DominionWeb.Game.Common
{
    
    //TODO: refactor to BinaryActionRequest to handle some other cards in Intrigue
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