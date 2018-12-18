namespace DominionWeb.Game.Cards
{
    public interface IOnGainOverride
    {
        void OnGain(IPlayer player, Card card);
    }
}