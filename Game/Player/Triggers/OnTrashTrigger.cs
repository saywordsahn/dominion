namespace DominionWeb.Game.Player.Triggers
{
    public class OnTrashTrigger : ITrigger
    {
        public Card TriggerCard { get; }
        
        public OnTrashTrigger(Card card)
        {
            TriggerCard = card;
        }
        
        public bool IsMet(PlayerAction playerAction, Card card)
        {
            return playerAction == PlayerAction.Trash
                   && (TriggerCard == Card.Any || card == TriggerCard);
        }
    }
}