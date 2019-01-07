using DominionWeb.Game.Cards.Abilities.Attacks.Effects;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class GainCard : IAbility, IAttackEffect
    {
        // Experiment requires ICard to hold state about whether or not it can
        // gain another card.
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
            //TODO: allow only one gain at a time to make on gain actions work in proper order
            if (CardToGain.Name == Card.Spoils)
            {
                //law of demeter broken, redesign?
                if (game.Components.Spoils.Count > 0)
                {
                    player.Gain(game.Components.Spoils.Take());
                    Resolved = true;
                }
            }
            else if (CardToGain.Name == Card.VirtualRuins)
            {
                if (game.Supply.IncludeRuins && game.Supply.RuinsPile.Cards.Count > 0)
                {
                    var topRuins = game.Supply.RuinsPile.Cards[game.Supply.RuinsPile.Cards.Count - 1];
                    game.Supply.Take(topRuins);
                    player.Gain(topRuins);
                    Resolved = true;
                }
                else if (game.Supply.IncludeRuins && game.Supply.RuinsPile.Cards.Count == 0)
                {
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