using System.Linq;
using DominionWeb.Game.Cards;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Common.Rules
{
    public class TakeAttackEffect : IRule
    {
        public void Resolve(Game game, IPlayer player)
        {
            var attackingPlayer = game.GetAttackingPlayer();
            var attackCard = (IAttack) attackingPlayer.PlayedCards.Last(x => x.Card is IAttack).Card;
            
            player.RuleStack.Push(attackCard.AttackEffect());

            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}