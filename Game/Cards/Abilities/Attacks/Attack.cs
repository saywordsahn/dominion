using System.Linq;
using DominionWeb.Game.Cards.Abilities.Attacks.Effects;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.Attacks
{
    public class Attack : IAbility
    {
        public IAttackEffect AttackEffect { get; set; }
        
        public Attack(IAttackEffect effect)
        {
            AttackEffect = effect;
        }

        public void Resolve(Game game, IPlayer player)
        {
            foreach (var p in game.Players.Where(x => x != player))
            {
                p.RuleStack.Push(new RespondToAttackRule(AttackEffect));
            }

            player.RuleStack.Push(new SetNextAttackedPlayer());
            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}