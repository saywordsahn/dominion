using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;
using DominionWeb.Models;

namespace DominionWeb.Game.Cards.Abilities
{
    public class TrashFromSupply : IAbility, IResponseRequired<IEnumerable<Card>>
    {

        public ICardFilter Filter;
        public bool Resolved { get; set; }

        public TrashFromSupply(ICardFilter filter)
        {
            Filter = filter;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            var selectableCards = game.Supply.GetDistinctCards()
                .Select(CardFactory.Create)
                .Where(Filter.Apply)
                .Select(x => x.Name)
                .ToList();

            if (selectableCards.Count == 0)
            {
                player.PlayStatus = PlayStatus.ActionPhase;
                Resolved = true;
            }
            else
            {
                player.ActionRequest = new SelectCardsActionRequest("Select a card to trash.",
                    Card.Lurker, selectableCards, 1);
                player.PlayStatus = PlayStatus.ActionRequestResponder;
            }
        }

        
        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var player = game.GetActivePlayer();

            var cardList = response.ToList();

            if (cardList.Count == 1)
            {
                var instance = CardFactory.Create(cardList[0]);

                if (Filter.Apply(instance))
                {
                    game.Supply.Take(instance.Name);
                    game.Supply.AddToTrash(instance.Name);
                    player.PlayStatus = PlayStatus.ActionPhase;
                    Resolved = true;
                }
                
            }
        }
    }
}