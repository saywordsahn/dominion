using System.Collections.Generic;

namespace DominionWeb.Game.Common
{
    public class SelectCardsActionRequest : IActionRequest
    {
        public ActionRequestType ActionRequestType { get; } = ActionRequestType.SelectCards;
        public string Message { get; }
        public Card Requester { get; }
        
        public int MaxCardsSelectable { get; private set; }
        public IEnumerable<Card> Cards { get; private set; }

        public SelectCardsActionRequest(string message, Card requester, IEnumerable<Card> cards, int maxCardsSelectable)
        {
            Message = message;
            Cards = cards;
            Requester = requester;
            MaxCardsSelectable = maxCardsSelectable;
        }
    }
}