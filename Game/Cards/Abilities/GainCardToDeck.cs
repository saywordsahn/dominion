using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    //TODO: consolidate GainCard, GainCardToDeck, and GainCardToHand (GainCardFromTrash)?
    public class GainCardToDeck : IAbility
    {
        public Card CardToGain { get; set; }
        public int Amount { get; set; }
        public bool Resolved { get; set; }

        public GainCardToDeck(Card card, int amount = 1)
        {
            CardToGain = card;
            Amount = amount;
        }
        
        public void Resolve(IPlayer player)
        {
            player.GainToHand(CardToGain, Amount);
            Resolved = true;
        }

        public void Resolve(Game game, IPlayer player)
        {

            for (var i = 0; i < Amount; i++)
            {
                if (!game.Supply.Contains(CardToGain)) break;
                game.Supply.Take(CardToGain);
                player.Deck.Add(CardToGain);
            }
            
            Resolved = true;
        }

        
        
    }
}