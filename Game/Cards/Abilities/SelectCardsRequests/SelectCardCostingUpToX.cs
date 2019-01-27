using System.Linq;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.SelectCardsRequests
{
    public class SelectCardCostingUpToX : IAbility
    {
        public int X { get; set; }
        public IAbility Parent { get; set; }

        public SelectCardCostingUpToX(IAbility parent, int x)
        {
            Parent = parent;
            X = x;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            var selectableCards = game.Supply.GetGainableCards()
                .Select(CardFactory.Create)
                .Where(x => x.Cost <= X)
                .Select(x => x.Name)
                .ToList();

            if (selectableCards.Count == 0)
            {
                player.PlayStatus = PlayStatus.ActionPhase;
                Parent.Resolved = true;
                Resolved = true;
            }
            else
            {
                player.ActionRequest = new SelectCardsActionRequest("Gain a card costing up to " + X + ".",
                    Card.Armory, selectableCards, 1);
                player.PlayStatus = PlayStatus.ActionRequestResponder;
            }
            
            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}