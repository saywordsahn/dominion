using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Hinterlands
{
    public class NomadCamp : ICard, IAction, IOnGainOverride
    {
        public Card Name { get; } = Card.NomadCamp;
        public int Cost { get; } = 4;
        public CardType CardType { get; } = CardType.Action;
        
        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();

            player.NumberOfBuys++;
            player.MoneyPlayed += 2;
        }

        public void OnGain(IPlayer player, Card card)
        {
            player.Deck.Add(card);
        }
    }
}