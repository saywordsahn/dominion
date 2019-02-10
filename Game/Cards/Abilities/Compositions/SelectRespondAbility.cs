using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Abilities.SelectCardsResponses;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.Compositions
{
	public class SelectRespondAbility : IAbility, IResponseRequired<IEnumerable<Card>>
    {
        IAbility SelectCardsRequest { get; set; }
        IInputRequiredAbility<ICard> Response { get; set; }

        public SelectRespondAbility(IAbility selectCardsRequest, IInputRequiredAbility<ICard> response)
        {
            SelectCardsRequest = selectCardsRequest;
            Response = response;
        }

        public bool Resolved { get; set; }

        public void Resolve(Game game, IPlayer player)
        {
            SelectCardsRequest.Resolve(game, player);
        }

        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var player = game.GetActivePlayer();

            var card = response.ToList();

            if (card.Count == 1) 
            {
                var instance = CardFactory.Create(card[0]);

                Response.SetInput(instance);

                player.RuleStack.Push(Response);

                player.PlayStatus = PlayStatus.ActionPhase;
                Resolved = true;
            }


        }
    }
}
