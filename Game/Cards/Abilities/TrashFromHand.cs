using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Abilities.Types;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class TrashFromHand : IAbility, IResponseRequired<IEnumerable<Card>>, ICardSelector
    {
        public ICardFilter Filter;
        public bool Resolved { get; set; }
        public int Amount { get; set; }
        public ICard SelectedCard { get; set; }

        public TrashFromHand(ICardFilter filter, int amount = 1)
        {
            Filter = filter;
            Amount = amount;
        }

        //TODO: need to set this as resolved if SelectCardFromHand resolves early (there are no cards in hand)
        public void Resolve(Game game, IPlayer player)
        {
            string message;
            
            var selectableCards = player.Hand
                .Select(CardFactory.Create)
                .Where(Filter.Apply)
                .Select(x => x.Name)
                .ToList();

            if (selectableCards.Count == 0)
            {
                Resolved = true;
            }
            if (selectableCards.Count > 0 && selectableCards.Count <= Amount)
            {
                //trash automatically and move on
                foreach (var card in selectableCards)
                {
                    player.TrashFromHand(game.Supply, card);
                }

                Resolved = true;
            }
            else
            {
                if (Amount == 1)
                {
                    message = "Select a card to trash";
                }
                else
                {
                    message = "Select " + Amount + " cards to trash.";
                }
            
                new SelectCardFromHand(Filter, message)
                    .Resolve(game, player);
            }            
        }

        //TODO: need to include reactions to trashing
        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var player = game.GetActivePlayer();

            var cardList = response.ToList();

            if (cardList.Count == Amount)
            {
                foreach (var card in cardList)
                {
                    var instance = CardFactory.Create(card);

                    if (Filter.Apply(instance))
                    {
                        SelectedCard = instance;
                        player.TrashFromHand(game.Supply, instance.Name);
                        player.PlayStatus = PlayStatus.ActionPhase;
                        Resolved = true;
                    }
                }
            }
        }
    }
}