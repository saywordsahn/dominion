using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class SeerAbility : IAbility
    {
        public void Resolve(Game game, IPlayer player)
        {
            //reveal top 3 cards
            var topCards = player.GetTopCards(3);

            int count = 3;
            //put 2-4 cost in hand
            foreach (var card in topCards)
            {
                player.GameLog.Add(player.PlayerName.Substring(0,1) + " reveals " + card);
                var instance = CardFactory.Create(card);
                if (instance.Cost >= 2 && instance.Cost <= 4)
                {
                    //remove from deck
                    player.GameLog.Add(player.PlayerName.Substring(0,1) + " adds " + card + " to hand");
                    player.Deck.Remove(card);
                    player.Hand.Add(card);
                    count--;
                }
            }

            if (count > 1)
            {
                player.RuleStack.Push(new OrderTopOfDeck(count));
                Resolved = true;
            }
            else
            {
                Resolved = true;
            }           
        }

        public bool Resolved { get; set; }
    }
}