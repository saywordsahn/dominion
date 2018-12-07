namespace DominionWeb.Game.Common
{
    public interface IActionRequester
    {
        void ResponseReceived(Game game, ActionResponse response);
    }
}