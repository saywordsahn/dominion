using System.Collections.Generic;
using DominionWeb.Game.Cards;

namespace DominionWeb.Game.Common.Requests
{
    public class OrderCardsRequest : IActionRequest
    {
        public ActionRequestType ActionRequestType { get; } = ActionRequestType.OrganizeCards;
        public string Message { get; }
        public Card Requester { get; }        
        public IEnumerable<Card> Cards { get; set; }

        public OrderCardsRequest(string message, Card requester, IEnumerable<Card> cards)
        {
            Message = message;
            Cards = cards;
            Requester = requester;
        }
    }
}