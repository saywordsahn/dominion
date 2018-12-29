using System.Linq;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class PatricianAbility : IAbility
    {
        public bool Resolved { get; set; }

        public void Resolve(Game game, IPlayer player)
        {
            if (player.HasDrawableCards())
            {
                var topCard = player.TopCard();

                var instance = CardFactory.Create(topCard);

                if (instance.Cost >= 5)
                {
                    //Add without drawing (in case of triggers)
                    player.Hand.Add(topCard);
                    player.Deck.RemoveAt(player.Deck.Count - 1);
                }
                
            }

            Resolved = true;
        }

    }
}