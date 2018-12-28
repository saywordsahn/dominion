using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class GainCard : IAbility
    {
        public Card CardToGain { get; set; }
        public int Amount { get; set; }
        public bool Resolved { get; set; }

        public GainCard(Card card, int amount)
        {
            CardToGain = card;
            Amount = amount;
        }

        public void Resolve(Game game, IPlayer player)
        {
            //TODO: include all nonSupply cards in this logic
            if (CardToGain == Card.Spoils)
            {
                //law of demeter broken, redesign?
                if (game.Components.Spoils.Count > 0)
                {
                    player.Gain(game.Components.Spoils.Take());
                    Resolved = true;
                }
            }
            else
            {
                for (var i = 0; i < Amount; i++)
                {
                    if (!game.Supply.Contains(CardToGain)) break;
                    game.Supply.Take(CardToGain);
                    player.Gain(CardToGain);
                }
            
                Resolved = true;
            }
            
        }

        
        
    }
}