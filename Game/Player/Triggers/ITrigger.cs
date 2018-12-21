namespace DominionWeb.Game.Player.Triggers
{
    public interface ITrigger
    {
        //there will likely be other trigger types
        //this interface will be subject to change
        bool IsMet(PlayerAction playerAction, Card card);
    }
}