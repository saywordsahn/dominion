using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.Attacks.Effects
{
    public class GainRuinsAttackEffect : IAttackEffect
    {
        public void Resolve(Game game, IPlayer player)
        {
            player.RuleStack.Push(new GainCard(Card.VirtualRuins));
        }

        public bool Resolved { get; set; }
    }
}