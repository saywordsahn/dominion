namespace DominionWeb.Game.Cards.Abilities
{
    public interface IResponseRequired<in T>
    {
        void ResponseReceived(Game game, T response);
    }
}