using System;

namespace DominionWeb.Game.Player
{
    public class OnPlayTrigger : ITrigger
    {
        public Card TriggerCard { get; }
        
        public OnPlayTrigger(Card card)
        {
            TriggerCard = card;
        }
        
        public bool IsMet(PlayerAction playerAction, Card card)
        {
            return playerAction == PlayerAction.Play && card == TriggerCard;
        }
    }
}