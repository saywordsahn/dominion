using System;
using System.Linq;
using DominionWeb.Game.Cards;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Common.Rules
{
    public class SetNextAttackedPlayer : IRule
    {

        public SetNextAttackedPlayer()
        {
            
        }
        
        /// <summary>
        /// Given that an Attacker has been set, this will set the next attacked player.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="player"></param>
        public void Resolve(Game game, IPlayer player)
        {
            player.PlayStatus = PlayStatus.Attacker;
            
            if (!game.Players.Any(x => x.PlayStatus == PlayStatus.Attacker))
                throw new InvalidOperationException("There must be an user with PlayStatus.Attacker to set the next attacked player.");
                
            //add reactions back to hand
            //do this outside of SetNextAttackedPlayer?
            player.Hand.AddRange(player.PlayedReactions);
            
            AttackNextPlayer(game, player);

        }
        
        public void AttackNextPlayer(Game game, IPlayer currentPlayer)
        {
            var nextPlayer = game.GetNextPlayer(currentPlayer);

            if (nextPlayer == currentPlayer)
            {
                // if there is only one player
                nextPlayer.PlayStatus = PlayStatus.ActionPhase;
            }
            else if (nextPlayer.PlayStatus == PlayStatus.Attacker)
            {
                currentPlayer.PlayStatus = PlayStatus.WaitForTurn;
                nextPlayer.PlayStatus = PlayStatus.ActionPhase;
            }
            else if (currentPlayer.PlayStatus == PlayStatus.Attacker)
            {
                nextPlayer.PlayStatus = PlayStatus.AttackResponder;
                //nextPlayer.SetAttacked(game);
            }
            else
            {
                currentPlayer.PlayStatus = PlayStatus.WaitForTurn;
                nextPlayer.PlayStatus = PlayStatus.AttackResponder;
                //nextPlayer.SetAttacked(game);  
            }

            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}