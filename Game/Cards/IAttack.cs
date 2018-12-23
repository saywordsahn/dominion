using DominionWeb.Game.Cards.AttackEffects;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards
{
    public interface IAttack
    {
        IAttackEffect AttackEffect();
        void AttackNextPlayer(Game game, IPlayer currentPlayer);
    }
}