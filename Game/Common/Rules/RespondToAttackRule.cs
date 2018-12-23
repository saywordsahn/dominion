using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Common.Rules
{
    //TODO: expand this rule? rename to something more fitting (like PlayReacationsRule)?
    public class RespondToAttackRule : IRule, IResponseRequired<IEnumerable<Card>>
    {
        public bool Resolved { get; set; }
        
        public void Resolve(Game game, IPlayer player)
        {
            //choose to reveal a attackReaction (moat, diplomat) before attack effect
            if (player.HasAttackReactionInHand())
            {
                var reactions = player.Hand.Select(CardFactory.Create)
                    .Where(x => x is IAttackReaction)
                    .Select(x => x.Name)
                    .ToList();
                
                player.ActionRequest =  new SelectCardsActionRequest(
                    "Select a reaction card to play.",
                    Card.None,
                    reactions,
                    1);

                player.PlayStatus = PlayStatus.ActionRequestResponder;
            }
            else
            {
                player.RuleStack.Push(new SetNextAttackedPlayer());
                player.RuleStack.Push(new TakeAttackEffect());
                Resolved = true;
            }

            
        }

        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            //here we can select the reaction card to be played
            //and check whether or not other reaction cards remain to be played
            var player = game.GetActivePlayer();

            var cardList = response.ToList();

            if (cardList.Count == 0)
            {
                //take attack effect 
                throw new NotImplementedException();
            }
            else if (cardList.Count == 1)
            {
                var card = CardFactory.Create(cardList[0]);

                if (card is IAttackBlocker)
                {
                    //attack is blocked. move to next player despite other attackResponse
                    //set next attacked player
                    throw new NotImplementedException();
                }
                else if (card is IAttackReaction ar)
                {
                    player.PlayStatus = PlayStatus.AttackResponder;
                    player.RuleStack.Push(new RespondToAttackRule());
                    player.RuleStack.Push(ar.ReactionEffect());

                    Resolved = true;
                }

            }
            
        }


    }
}