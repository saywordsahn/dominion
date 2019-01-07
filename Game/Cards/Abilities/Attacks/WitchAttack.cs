using System.Linq;
using DominionWeb.Game.Cards.Abilities.Attacks.Effects;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.Attacks
{
    public class WitchAttack : IAbility
    {
        public void Resolve(Game game, IPlayer player)
        {

            foreach (var p in game.Players.Where(x => x != player))
            {
                p.RuleStack.Push(new RespondToAttackRule(new GainCurseAttackEffect()));
            }
            
//            var nextPlayer = game.GetNextPlayer(player);
//            nextPlayer.PlayStatus = PlayStatus.AttackResponder;
//            nextPlayer.SetAttacked(game);
            player.RuleStack.Push(new SetNextAttackedPlayer());
            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}