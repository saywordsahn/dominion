using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.Attacks.Effects
{
    public class DiscardDownToX : IAttackEffect, IResponseRequired<IEnumerable<Card>>
    {
        public int X { get; set; }

        public DiscardDownToX(int x)
        {
            X = x;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            if (player.Hand.Count <= X)
            {
                Resolved = true;
                return;
            }

            var amountToDiscard = player.Hand.Count - X;
            
            
            player.ActionRequest = new SelectCardsActionRequest("Select " + amountToDiscard + " cards to be discarded.", Card.None,
                player.Hand, amountToDiscard);
            player.PlayStatus = PlayStatus.ActionRequestResponder;
        }

        public bool Resolved { get; set; }

        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var activePlayer = game.GetActivePlayer();
            var cardList = response.ToList();

            if (cardList.Count == activePlayer.Hand.Count - X)
            {
                //TODO: we may want to ruleize this so discard effects occur in the correct order
                foreach (var card in cardList)
                {
                    activePlayer.DiscardFromHand(card);
                }
            
                Resolved = true;
            }
            
            activePlayer.PlayStatus = PlayStatus.AttackResponder;
        }
    }
}