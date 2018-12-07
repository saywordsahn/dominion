namespace DominionWeb.Game.Cards
{
    public interface IAction
    {
        void Resolve(Game game);
    }
}