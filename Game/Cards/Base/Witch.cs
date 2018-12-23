using DominionWeb.Game.Cards.AttackEffects;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Base
{
    public class Witch : ICard, IAction, IAttack
    {
        public int Cost { get; } = 5;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Witch;

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.PlayStatus = PlayStatus.Attacker;
            
            player.Draw(2);

            var nextPlayer = game.GetNextPlayer(player);
            nextPlayer.PlayStatus = PlayStatus.AttackResponder;
            nextPlayer.SetAttacked(game);
        }

        public IAttackEffect AttackEffect() => new GainCurseAttackEffect();

        public void AttackNextPlayer(Game game, IPlayer currentPlayer)
        {
            var nextPlayer = game.GetNextPlayer(currentPlayer);

            if (nextPlayer == game.GetAttackingPlayer())
            {
                currentPlayer.PlayStatus = PlayStatus.WaitForTurn;
                nextPlayer.PlayStatus = PlayStatus.ActionPhase;
            }
            else
            {
                currentPlayer.PlayStatus = PlayStatus.WaitForTurn;
                nextPlayer.PlayStatus = PlayStatus.AttackResponder;
                nextPlayer.SetAttacked(game);  
            }
        }
    }
}