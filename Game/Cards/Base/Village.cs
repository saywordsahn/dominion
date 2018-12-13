namespace DominionWeb.Game.Cards.Base
{
    public class Village : ICard, IAction
    {
        public int Cost { get; } = 3;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Village;

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.Draw(1);
            player.NumberOfActions += 2;
            player.PlayStatus = PlayStatus.ActionPhase;
        }
        
    }
}