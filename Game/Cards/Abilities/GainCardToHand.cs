using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class GainCardToHand : IAbility, IRule
    {
        public Card CardToGain { get; set; }
        public int Amount { get; set; }
        public bool Resolved { get; set; }

        public GainCardToHand(Card card, int amount)
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
            player.GainToHand(CardToGain, Amount);
            Resolved = true;
        }

        
        
    }
}