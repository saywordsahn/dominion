using System;
using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.AttackEffects;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Base
{
    public class Militia : ICard, IAction, IAttack
    {
        public int Cost { get; } = 4;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Militia;

        public void Resolve(Game game)
        {
            //TODO: verify correctness
            var attacker = game.GetActivePlayer();
            attacker.PlayStatus = PlayStatus.Attacker;

            attacker.MoneyPlayed += 2;

            var nextPlayer = game.GetNextPlayer(attacker);
            nextPlayer.PlayStatus = PlayStatus.AttackResponder;
            nextPlayer.SetAttacked(game);
        }

        private bool PlayerCanBeAffected(IPlayer player)
        {
            //TODO: implement check from duration cards
            return player.Hand.Count > 3;
        }

        public IAttackEffect AttackEffect() => new DiscardDownToX(3);

        //TODO: this should be refactored as a Rule not associated with a card
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