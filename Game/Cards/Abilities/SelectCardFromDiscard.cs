using System.Linq;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class SelectCardFromDiscard : IAbility
    {
        public bool Resolved { get; set; }
        public string Message { get; set; }
        public ICardFilter Filter { get; set; }
        public bool DistinctCards { get; set; }

        public SelectCardFromDiscard(ICardFilter filter, string message, bool distinctCards = false)
        {
            Filter = filter;
            Message = message;
            DistinctCards = distinctCards;
        }

        public void Resolve(Game game, IPlayer player)
        {
            var selectableCards = player.DiscardPile
                .Select(CardFactory.Create)
                .Where(Filter.Apply)
                .Select(x => x.Name)
                .ToList();

            if (DistinctCards == true) {
                selectableCards = selectableCards.Distinct().ToList();
            }

            if (selectableCards.Count == 0)
            {
                player.PlayStatus = PlayStatus.ActionPhase;
                //TODO: find more elegant way to do this
                var parent = player.Rules.Last(x => x.Resolved == false);
                parent.Resolved = true;
                Resolved = true;
            }
            else
            {
                player.ActionRequest = new SelectCardsActionRequest(Message,
                    Card.Lurker, selectableCards, 1);
                player.PlayStatus = PlayStatus.ActionRequestResponder;
                Resolved = true;
            }

        }

    }
}