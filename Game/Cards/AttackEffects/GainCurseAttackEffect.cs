using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.AttackEffects
{
    //TODO: refactor this as gainCardAttackEffect or other
    public class GainCurseAttackEffect : IAttackEffect
    {
        public bool Resolved { get; set; }
        
        public void Resolve(Game game, IPlayer player)
        {
            if (!game.Supply.Contains(Card.Curse)) return;
            
            player.Gain(Card.Curse);
            game.Supply.Take(Card.Curse);
            Resolved = true;
        }
    }
}