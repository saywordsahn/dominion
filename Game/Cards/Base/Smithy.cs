namespace DominionWeb.Game.Cards.Base
{
    public class Smithy : ICard, IAction
    {
        public int Cost { get; } = 4;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Smithy;

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.Draw(3);
        }
        
    }
}