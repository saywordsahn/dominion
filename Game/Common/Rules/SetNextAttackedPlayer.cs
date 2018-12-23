using System.Linq;
using DominionWeb.Game.Cards;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Common.Rules
{
    public class SetNextAttackedPlayer : IRule
    {
        public void Resolve(Game game, IPlayer player)
        {
            //add reactions back to hand
            player.Hand.AddRange(player.PlayedReactions);
            
            var attackingPlayer = game.GetAttackingPlayer();
            
            var attackCard = (IAttack) attackingPlayer.PlayedCards.Last(x => x.Card is IAttack).Card;

            attackCard.AttackNextPlayer(game, player);

            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}