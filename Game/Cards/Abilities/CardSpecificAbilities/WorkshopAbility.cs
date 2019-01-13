using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Abilities.SelectCardsRequests;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class WorkshopAbility : IAbility, IResponseRequired<IEnumerable<Card>>
    {
        public void Resolve(Game game, IPlayer player)
        {
            new SelectCardCostingUpToX(this, 4).Resolve(game, player);
        }

        public bool Resolved { get; set; }
        
        public void ResponseReceived(Game game, IEnumerable<Card> responses)
        {
            var player = game.GetActivePlayer();
            
            var cardList = responses.ToList();

            if (cardList.Count == 1)
            {
                player.RuleStack.Push(new GainCard(cardList[0]));
                player.PlayStatus = PlayStatus.ActionPhase;
                Resolved = true;
            }
        }
    }
}