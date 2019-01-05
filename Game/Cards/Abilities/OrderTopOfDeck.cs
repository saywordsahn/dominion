using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Common.Requests;
using DominionWeb.Game.Player;
using DominionWeb.Game.Utils;

namespace DominionWeb.Game.Cards.Abilities
{
    public class OrderTopOfDeck : IAbility, IResponseRequired<IEnumerable<Card>>
    {
        public bool Resolved { get; set; }
        public int NumberOfCards { get; set; }

        public OrderTopOfDeck(int numberOfCards)
        {
            NumberOfCards = numberOfCards;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            //process as list for JsonSerializer
            var cards = player.GetTopCards(2).ToList();
            player.ActionRequest = new OrderCardsRequest("Reorder cards", Card.Survivors, cards);
            player.PlayStatus = PlayStatus.ActionRequestResponder;
        }        
        
        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var player = game.GetActivePlayer();

            var cards = response.ToList();

            //TODO: add check from actual top cards
            if (cards.Count == NumberOfCards)
            {
                player.Deck.RemoveFrom(player.Deck.Count - NumberOfCards);
                player.Deck.AddRange(cards);
                player.PlayStatus = PlayStatus.ActionPhase;
                Resolved = true;
            }
            
//
//            var cardList = response.ToList();
//            
//            if (cardList.Count != Cards.Count)
//            {
//                
//                Resolved = true;
//            }
        }
    }
}