using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class PookaAbility : IAbility, IResponseRequired<IEnumerable<Card>>
    {
        public ICardFilter Filter { get; set; }
        public PookaAbility()
        {
            Filter = new PookaFilter();
        }
        public void Resolve(Game game, IPlayer player)
        {
            var selectableCards = player.Hand
                .Select(CardFactory.Create)
                .Where(Filter.Apply)
                .Select(x => x.Name)
                .ToList();        
                                  
            if (selectableCards.Count == 0)
            {
                Resolved = true;
            }
            else
            {
                player.ActionRequest = new SelectCardsActionRequest("Select a card to trash",
                    Card.Pooka, selectableCards, 1);
                player.PlayStatus = PlayStatus.ActionRequestResponder;
            }
        }

        public bool Resolved { get; set; }
        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var player = game.GetActivePlayer();

            var cardList = response.ToList();

            if (cardList.Count == 1)
            {
                var instance = CardFactory.Create(cardList[0]);

                if (Filter.Apply(instance))
                {
                    player.TrashFromHand(game.Supply, instance.Name);
                    player.RuleStack.Push(new PlusCards(4));
                    player.PlayStatus = PlayStatus.ActionPhase;
                    Resolved = true;
                }
            }
            else if (cardList.Count == 0)
            {
                Resolved = true;
            }
        }
    }
}