namespace DominionWeb.Game.Cards.Prosperity
{
    public class Monument : ICard, IAction
    {
        public int Cost { get; } = 5;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Monument;

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.MoneyPlayed++;
            player.VictoryTokens++;
        }
    }
}