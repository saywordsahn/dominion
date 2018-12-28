using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class GainFromTrash : IAbility, IResponseRequired<IEnumerable<Card>>
    {
        public bool Resolved { get; set; }
        
        //json doesn't know how to serialize/deserialize this
        public ICardFilter Filter { get; set; }

        public GainFromTrash(ICardFilter filter)
        {
            Filter = filter;
        }

        public void Resolve(Game game, IPlayer player)
        {
            var selectableCards = game.Supply.Trash
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
                player.ActionRequest = new SelectCardsActionRequest("Select a card to gain.",
                    Card.None, selectableCards, 1);
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
                    game.Supply.Trash.Remove(instance.Name);
                    player.Gain(instance.Name);
                    player.PlayStatus = PlayStatus.ActionPhase;
                    Resolved = true;
                }
                
            }
            
        }
    }
}