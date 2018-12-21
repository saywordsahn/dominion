namespace DominionWeb.Game.Cards.Base
{
    public class Laboratory : ICard, IAction
    {
        public int Cost { get; } = 5;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Laboratory;

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.Draw(2);
            player.NumberOfActions++;
        }
    }
}