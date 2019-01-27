using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Abilities.SelectCardsRequests;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class GainCardCostingUpToX : IAbility, IResponseRequired<IEnumerable<Card>>
    {
		public int X {get; set;}
        public bool Resolved { get; set; }
        public GainTarget GainTarget { get; set; }

        public GainCardCostingUpToX(int x, GainTarget gainTarget = GainTarget.DiscardPile)
		{
            X = x;
            GainTarget = gainTarget;
		}

        public void Resolve(Game game, IPlayer player)
        {
            new SelectCardCostingUpToX(this, X).Resolve(game, player);
        }

        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var player = game.GetActivePlayer();

            var cardList = response.ToList();

            if (cardList.Count == 1)
            {
                var instance = CardFactory.Create(cardList[0]);

                if (instance.Cost > X) return;

                switch (GainTarget)
                {
                    case GainTarget.DiscardPile:
                        player.RuleStack.Push(new GainCard(cardList[0]));
                        break;
                    case GainTarget.Deck:
                        player.RuleStack.Push(new GainCardToDeck(cardList[0]));
                        break;
                    case GainTarget.Hand:
                        player.RuleStack.Push(new GainCardToHand(cardList[0]));
                        break;
                    default:
                        throw new InvalidOperationException("You cannot gain a card to that target");
                }

                player.PlayStatus = PlayStatus.ActionPhase;
                Resolved = true;
            }
        }
    }
}
