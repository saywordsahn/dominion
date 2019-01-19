using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class Discard : IAbility, IResponseRequired<IEnumerable<Card>>, IDiscardAbility
    {
        public int Amount { get; set; }
        public Discard(int amount)
        {
            Amount = amount;
            CardsDiscarded = 0;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            if (player.Hand.Count == 0)
            {
                Resolved = true;
            }
            else if (player.Hand.Count <= Amount)
            {
                foreach (var card in player.Hand.ToList())
                {
                    player.DiscardFromHand(card);
                }

                Resolved = true;
            }
            else
            {
                player.ActionRequest = new SelectCardsActionRequest("Select " + Amount + " cards to be discarded.", Card.None,
                    player.Hand, Amount);
                player.PlayStatus = PlayStatus.ActionRequestResponder;
            }
            
        }

        public bool Resolved { get; set; }
        
        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var player = game.GetActivePlayer();

            var cardList = response.ToList();

            if (cardList.Count == Amount)
            {
                foreach (var card in cardList)
                {
                    player.DiscardFromHand(card);
                    CardsDiscarded++;
                }
                
                player.PlayStatus = PlayStatus.ActionPhase;
                Resolved = true;
            }
        }

        public int CardsDiscarded { get; set; }
    }
}