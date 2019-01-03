using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class GainCard : IAbility
    {
        public ICard CardToGain { get; set; }
        public int Amount { get; set; }
        public bool Resolved { get; set; }

        public GainCard()
        {
            //default necessary for JsonSerializer
        }
        public GainCard(Card card, int amount = 1)
        {
            CardToGain = CardFactory.Create(card);
            Amount = amount;
        }

        public GainCard(ICard card, int amount = 1)
        {
            CardToGain = card;
            Amount = amount;
        }

        public void Resolve(Game game, IPlayer player)
        {
            //TODO: include all nonSupply cards in this logic
            if (CardToGain.Name == Card.Spoils)
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
                    if (!game.Supply.Contains(CardToGain.Name)) break;
                    game.Supply.Take(CardToGain.Name);
                    player.Gain(CardToGain);
                }
            
                Resolved = true;
            }
            
        }

        
        
    }
}