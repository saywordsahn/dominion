using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards
{
    public interface IAttack
    {
        void AttackEffect(IPlayer attackedPlayer, Game game);
    }
}