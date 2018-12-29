using System.Linq;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class TrashCard : IAbility
    {
        public CardLocation CardLocation { get; set; }
        public Card Card { get; set; }
        public bool Resolved { get; set; }

        
        public TrashCard(CardLocation cardLocation, Card card)
        {
            CardLocation = cardLocation;
            Card = card;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            if (CardLocation == CardLocation.PlayedCards)
            {
                var playedCard = player.PlayedCards.Last(x => x.Card.Name == Card);
                player.PlayedCards.Remove(playedCard);
                player.GameLog.Add(player.PlayerName.Substring(0, 1) + " trashes a " + playedCard.Card.Name.ToString());
                game.Supply.AddToTrash(playedCard.Card.Name);            
                player.RunTriggeredAbilities(PlayerAction.Trash, playedCard.Card.Name);
                Resolved = true;
            }
        }

    }
}