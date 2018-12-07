namespace DominionWeb.Game.Cards
{
    public interface ICard
    {
        Card Name { get; }
        int Cost { get; }
        CardType CardType { get; }
    }
}