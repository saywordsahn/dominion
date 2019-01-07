using System;
using System.Security.Cryptography.X509Certificates;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    //TODO: consider adding this to rules?
    public class PlayActionCard : IAbility
    {
        public PlayedCard CardToPlay { get; set; }

        public PlayActionCard(PlayedCard cardToPlay)
        {
            if (!(cardToPlay.Card is IAction)) throw new ArgumentException("Card must be and action card.");
            CardToPlay = cardToPlay;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            player.RunTriggeredAbilities(PlayerAction.Play, CardToPlay.Card.Name);
            
            if (CardToPlay.IsThronedCopy == false)
            {
                player.Hand.Remove(CardToPlay.Card.Name);
            }
            
            //TODO: update to action after rulesholder is removed
            if (CardToPlay.Card is IRulesHolder rh)
            {
                player.PlayedCards.Add(CardToPlay);

                foreach (var ability in rh.GetRules(game, player))
                {
                    player.RuleStack.Push(ability);
                }
            }
            
            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}