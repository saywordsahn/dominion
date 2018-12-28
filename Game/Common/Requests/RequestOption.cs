namespace DominionWeb.Game.Common.Requests
{
    public class RequestOption
    {
        public ActionResponse ActionResponse { get; set; }
        public string ActionResponseText { get; set; }

        public RequestOption(ActionResponse actionResponse, string actionResponseText)
        {
            ActionResponse = actionResponse;
            ActionResponseText = actionResponseText;
        }
    }
}