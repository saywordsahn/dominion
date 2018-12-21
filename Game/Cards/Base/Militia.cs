using System;
using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Base
{
    public class Militia : ICard, IAction, IAttack, IResponseRequired<IEnumerable<Card>>
    {
        public int Cost { get; } = 4;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Militia;

        public void Resolve(Game game)
        {
            //TODO: verify correctness
            var attacker = game.GetActivePlayer();
            attacker.PlayStatus = PlayStatus.Attacker;

            attacker.MoneyPlayed += 2;


            var nextPlayer = game.GetNextPlayer(attacker);

            while (nextPlayer != attacker)
            {
                if (PlayerIsAffected(nextPlayer))
                {
                    break;
                }

                nextPlayer = game.GetNextPlayer(nextPlayer);
            }

            if (nextPlayer == attacker)
            {
                //nobody affected - continue
                attacker.PlayStatus = PlayStatus.ActionPhase;
            }
            else
            {
                if (nextPlayer.HasReactionInHand())
                {
                    //send action request to play reaction
                }
                else
                {
                    //player is forced to take attack
                }
            }
            
            //if so send respondToAttackRequest
            game.GetNextPlayer(attacker).PlayStatus = PlayStatus.Responder;
        }

        private bool PlayerIsAffected(IPlayer player)
        {
            //TODO: implement check from duration cards
            return player.Hand.Count > 3;
        }

        public void AttackEffect(IPlayer attackedPlayer, Game game)
        {
            if (attackedPlayer.Hand.Count <= 3)
            {
                
            }
                
        }

        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            throw new NotImplementedException();
        }
    }
}