using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class TrashFromHand : IAbility, IResponseRequired<IEnumerable<Card>>
    {

        public ICardFilter Filter;
        public bool Resolved { get; set; }

        public TrashFromHand(ICardFilter filter)
        {
            Filter = filter;
        }

        //TODO: need to set this as resolved if SelectCardFromHand resolves early (there are no cards in hand)
        public void Resolve(Game game, IPlayer player)
            => new SelectCardFromHand(new NoFilter(), "Select a card to trash.")
                .Resolve(game, player);


        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var player = game.GetActivePlayer();

            var cardList = response.ToList();

            if (cardList.Count == 1)
            {
                var instance = CardFactory.Create(cardList[0]);

                if (Filter.Apply(instance))
                {
                    player.Hand.Remove(instance.Name);
                    game.Supply.AddToTrash(instance.Name);
                    player.PlayStatus = PlayStatus.ActionPhase;
                    Resolved = true;
                }

            }
        }
    }
}