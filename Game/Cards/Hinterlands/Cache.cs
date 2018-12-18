namespace DominionWeb.Game.Cards.Hinterlands
{
    public class Cache: ICard, ITreasure, IOnGainAbilityResolver
    {
        public Card Name { get; } = Card.Cache;
        public int Cost { get; } = 5;
        public CardType CardType { get; } = CardType.Treasure;
        public int Value { get; } = 3;

        public void OnGain(Game game)
        {
            var player = game.GetActivePlayer();
            
            player.Gain(Card.Copper);
            player.Gain(Card.Copper);

        }
    }
}