namespace DominionWeb.Game.Cards.Base
{
    public class Smithy : ICard, IAction
    {
        private readonly Card _name = Card.Smithy;
        private readonly CardType _cardType = CardType.Action;
        private int _cost = 4;
    
        public int Cost => _cost;
        public CardType CardType => _cardType;
        public Card Name => _name;

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.Draw(3);
        }
        
    }
}