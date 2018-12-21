namespace DominionWeb.Game.Cards
{
    public class PlayedCard
    {
        public ICard Card { get; set; }
        public bool IsThronedCopy { get; set; } = false;

        public PlayedCard(ICard card, bool isThronedCopy = false)
        {
            Card = card;
            IsThronedCopy = isThronedCopy;
        }
    }
}