using System.Linq;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class SelectCardFromHand : IAbility
    {
        public bool Resolved { get; set; }
        public string Message { get; set; }
        public ICardFilter Filter { get; set; }
        
        public SelectCardFromHand(ICardFilter filter, string message)
        {
            Filter = filter;
            Message = message;
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
                player.PlayStatus = PlayStatus.ActionPhase;
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