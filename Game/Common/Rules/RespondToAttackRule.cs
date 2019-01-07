using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.Attacks.Effects;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Common.Rules
{
    //TODO: expand this rule? rename to something more fitting (like PlayReacationsRule)?
    public class RespondToAttackRule : IRule, IResponseRequired<IEnumerable<Card>>
    {
        public bool Resolved { get; set; }
        public IAttackEffect AttackEffect { get; set; }

        public RespondToAttackRule(IAttackEffect attackEffect)
        {
            AttackEffect = attackEffect;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            //check for duration attack blockers - like champion, lighthouse
            if (player.PlayedCards.Any(x => x.Card is IAttackBlocker && x.Card is IDuration))
            {
                player.RuleStack.Push(new SetNextAttackedPlayer());
                Resolved = true;
                return;
            }
            
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
                player.RuleStack.Push(AttackEffect);
                Resolved = true;
            }
        }

        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            //here we can select the reaction card to be played
            //and check whether or not other reaction cards remain to be played
            var player = game.GetActivePlayer();

            var cardList = response.ToList();

            //TODO: test no selected reaction
            if (cardList.Count == 0)
            {
                player.RuleStack.Push(new SetNextAttackedPlayer());
                player.RuleStack.Push(AttackEffect);
                Resolved = true;
            }
            else if (cardList.Count == 1)
            {
                var card = CardFactory.Create(cardList[0]);

                if (card is IAttackBlocker)
                {
                    //attack is blocked. move to next player despite other attackResponse
                    //set next attacked player
                    //TODO: implement show card (for moat)
                    player.RuleStack.Push(new SetNextAttackedPlayer());
                    Resolved = true;
                }
                else if (card is IAttackReaction ar)
                {
                    player.PlayStatus = PlayStatus.AttackResponder;
                    player.RuleStack.Push(new RespondToAttackRule(AttackEffect));
                    player.RuleStack.Push(ar.ReactionEffect());

                    Resolved = true;
                }

            }
            
        }


    }
}