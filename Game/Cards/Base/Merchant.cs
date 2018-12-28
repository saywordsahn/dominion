using DominionWeb.Game.Cards.Abilities.TriggeredAbilities;

namespace DominionWeb.Game.Cards.Base
{
    public class Merchant : ICard, IAction
    {
        public int Cost { get; } = 3;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Merchant;

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.Draw(1);
            player.NumberOfActions++;
            //TODO: test triggered ability more - there are bugs
            player.TriggeredAbilities.Add(new PlusOneMoneyOnFirstSilverPlay());
        }
        
        
        
    }
}