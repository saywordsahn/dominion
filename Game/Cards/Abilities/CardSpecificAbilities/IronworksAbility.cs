using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Abilities.SelectCardsRequests;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class IronworksAbility : IAbility, IResponseRequired<IEnumerable<Card>>
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
                var instance = CardFactory.Create(cardList[0]);

                if (instance is IAction)
                {
                    player.RuleStack.Push(new PlusActions(1));
                }

                if (instance is ITreasure)
                {
                    player.RuleStack.Push(new PlusMoney(1));
                }

                if (instance is IVictory)
                {
                    player.RuleStack.Push(new PlusCards(1));
                }
                
                player.RuleStack.Push(new GainCard(instance));
                player.PlayStatus = PlayStatus.ActionPhase;
                Resolved = true;
            }
        }
    }
}