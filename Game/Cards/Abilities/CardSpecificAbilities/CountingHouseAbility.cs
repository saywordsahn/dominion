using System.Linq;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class CountingHouseAbility : IAbility
    {
        public void Resolve(Game game, IPlayer player)
        {
            var numberOfCoppers = player.DiscardPile.RemoveAll(x => x == Card.Copper);

            if (numberOfCoppers == 0)
            {
                Resolved = true;
                return;
            }

            player.GameLog.Add(player.PlayerName.Substring(0,1) + " reveals " + numberOfCoppers + " coppers");
            
            player.Hand.AddRange(Enumerable.Repeat(Card.Copper, numberOfCoppers));

            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}