namespace DominionWeb.Game.Player.Triggers
{
    public class OnPlayTrigger : ITrigger
    {
        public Card TriggerCard { get; set; }
        
        public OnPlayTrigger(Card card)
        {
            TriggerCard = card;
        }
        
        public bool IsMet(PlayerAction playerAction, Card card)
        {
            return playerAction == PlayerAction.Play
                && (TriggerCard == Card.Any || card == TriggerCard);

        }
    }
}