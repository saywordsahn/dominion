using System.Collections.Generic;

namespace DominionWeb.Game.Common.Requests
{
    public class SelectOptionsActionRequest : IActionRequest
    {
        public ActionRequestType ActionRequestType { get; } = ActionRequestType.SelectOptions;
        public string Message { get; }
        public Card Requester { get; }
        
        public int MaxSelectable { get; private set; }
        
        public IEnumerable<RequestOption> Options { get; private set; }

        public SelectOptionsActionRequest(string message, Card requester, IEnumerable<RequestOption> options, int maxSelectable)
        {
            Message = message;
            Options = options;
            Requester = requester;
            MaxSelectable = maxSelectable;
        }
    }
}