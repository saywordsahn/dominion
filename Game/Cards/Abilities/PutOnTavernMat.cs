using System;
using System.Linq;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class PutOnTavernMat : IAbility
    {
        public Card Card { get; set; }
        public CardLocation CardLocation { get; set; }

        public PutOnTavernMat(CardLocation cardLocation, Card card)
        {
            Card = card;
            CardLocation = cardLocation;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            switch (CardLocation)
            {
                case CardLocation.PlayedCards:
                    var playedCard = player.PlayedCards.Last(x => x.Card.Name == Card && x.IsThronedCopy == false);
            
                    player.PlayedCards.Remove(playedCard);
                    player.GameLog.Add(player.PlayerName.Substring(0, 1) + " puts a " + playedCard.Card.Name
                                       + " on their Tavern Mat.");
                    player.TavernMat.Add(playedCard.Card.Name);
                    Resolved = true;
                    break;
                case CardLocation.Hand:
                    player.Hand.Remove(Card);
                    player.TavernMat.Add(Card);
                    player.GameLog.Add(player.PlayerName.Substring(0, 1) + " puts a " + Card
                                       + " on their Tavern Mat.");
                    Resolved = true;
                    break;
                default:
                    throw new NotImplementedException();
            }
            
                    
                
        }
        

        public bool Resolved { get; set; }
    }
}