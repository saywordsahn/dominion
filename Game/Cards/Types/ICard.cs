namespace DominionWeb.Game.Cards.Types
{
    public interface ICard
    {
        Card Name { get; }
        int Cost { get; }
        CardType CardType { get; }
    }
}