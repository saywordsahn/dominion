using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class RepeatCard : IAbility, IResponseRequired<IEnumerable<Card>>
    {
        public int NumberOfTimesToPlay { get; set; }

        public RepeatCard(int numberOfTimesToPlay)
        {
            NumberOfTimesToPlay = numberOfTimesToPlay;
        }
        
        public void Resolve(Game game, IPlayer player)
         => new SelectCardFromHand(new ActionFilter(), "Select a card to play.")
            .Resolve(game, player);

        public bool Resolved { get; set; }
        
        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var player = game.GetActivePlayer();
            var cardList = response.ToList();

            if (cardList.Count != 1) return;
            
            var instance = CardFactory.Create(cardList[0]);

            if (instance is IAction a)
            {
                //player.PlayWithoutCost(instance);

                for (var i = 0; i < NumberOfTimesToPlay - 1; i++)
                {
                    player.RuleStack.Push(new PlayActionCard(new PlayedCard(CardFactory.Create(cardList[0], true), true)));
                    
                    //player.PlayStack.Push(new PlayedCard(CardFactory.Create(cardList[0], true), true));
                }
                
                player.RuleStack.Push(new PlayActionCard(new PlayedCard(instance, false)));
//                player.PlayStack.Push(new PlayedCard(instance, false));
                player.PlayStatus = PlayStatus.ActionPhase;
                Resolved = true;
            }
        }
    }
}