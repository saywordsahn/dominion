using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
	public class PoacherAbility : IAbility, IResponseRequired<IEnumerable<Card>>
    {
        public PoacherAbility()
        {
        }

        public bool Resolved { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Resolve(Game game, IPlayer player)
        {
            player.Draw(1);
            player.NumberOfActions++;
            player.MoneyPlayed++;

            var emptyPileCount = game.Supply.EmptyPileCount();

            if (emptyPileCount > 0)
            {
                player.ActionRequest = new SelectCardsActionRequest("Discard " + emptyPileCount + " cards.",
                    Card.Poacher, player.Hand, emptyPileCount);
                player.PlayStatus = PlayStatus.ActionRequestResponder;
            }
        }

        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var player = game.GetActivePlayer();

            var cardList = response.ToList();

            if (cardList.Count != game.Supply.EmptyPileCount()) return;

            foreach (var c in response)
            {
                player.DiscardFromHand(c);
            }

            player.PlayStatus = PlayStatus.ActionPhase;
            Resolved = true;
        }
    }
}
