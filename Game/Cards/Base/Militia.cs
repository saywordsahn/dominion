using System;
using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.AttackEffects;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Base
{
    public class Militia : ICard, IAction, IAttack, IResponseRequired<IEnumerable<Card>>
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

            while (nextPlayer != attacker)
            {
                if (PlayerCanBeAffected(nextPlayer))
                {
                    break;
                }

                nextPlayer = game.GetNextPlayer(nextPlayer);
            }

            if (nextPlayer == attacker)
            {
                //nobody affected - continue
                attacker.PlayStatus = PlayStatus.ActionPhase;
            }
            else
            {
//                var cardsToDiscard = nextPlayer.Hand.Count <= 3 ? 0 : nextPlayer.Hand.Count - 3;
//                
//                nextPlayer.ActionRequest = new SelectCardsActionRequest("Select " + cardsToDiscard + " card to discard.",
//                    Card.Militia, nextPlayer.Hand, cardsToDiscard);

                nextPlayer.PlayStatus = PlayStatus.AttackResponder;
                nextPlayer.SetAttacked(game);               
            }
            
        }

        private bool PlayerCanBeAffected(IPlayer player)
        {
            //TODO: implement check from duration cards
            return player.Hand.Count > 3;
        }

        //TODO: implement militiaAttackEffect
        public IAttackEffect AttackEffect() => new GainCurseAttackEffect();

        public void AttackNextPlayer(Game game, IPlayer currentPlayer)
        {
            throw new NotImplementedException();
        }

        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            throw new NotImplementedException();
        }
    }
}