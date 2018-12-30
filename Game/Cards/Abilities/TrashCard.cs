using System.Linq;
using DominionWeb.Game.Cards.Types;
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
        
        //TODO: find a way to merge this and player.TrashFromHand
        public void Resolve(Game game, IPlayer player)
        {
            if (CardLocation == CardLocation.PlayedCards)
            {
                var playedCard = player.PlayedCards.Last(x => x.Card.Name == Card);
                player.PlayedCards.Remove(playedCard);
                player.GameLog.Add(player.PlayerName.Substring(0, 1) + " trashes a " + playedCard.Card.Name.ToString());
                game.Supply.AddToTrash(playedCard.Card.Name);

                if (playedCard.Card is IOnTrashAbilityHolder abilityHolder)
                {
                    abilityHolder.ResolveOnTrashAbilities(player);    
                }
                
                player.RunTriggeredAbilities(PlayerAction.Trash, playedCard.Card.Name);
                Resolved = true;
            }
        }

    }
}