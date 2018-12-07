using DominionWeb.Game.Cards;

namespace DominionWeb.Game.Common
{
    public interface IActionRequest
    {
        ActionRequestType ActionRequestType { get; }
        string Message { get; }
        Card Requester { get; }
    }
}