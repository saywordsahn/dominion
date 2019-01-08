using System.Linq;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class SettlersBustlingVillageAbility : IAbility
    {
        public Card CardToFind { get; set; }

        public SettlersBustlingVillageAbility(Card cardToFind)
        {
            CardToFind = cardToFind;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            //TODO: implement discard searching

            if (player.DiscardPile.Contains(CardToFind))
            {
                //add card to hand
                player.GameLog.Add(player.PlayerName.Substring(0, 1) + " reveals a " + CardToFind);
                player.DiscardPile.Remove(CardToFind);
                player.GameLog.Add(player.PlayerName.Substring(0, 1) + " adds a " + CardToFind + " to their hand");
                player.Hand.Add(CardToFind);
            }            

            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}