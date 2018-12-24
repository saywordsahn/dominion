using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.TriggeredAbilities;
using DominionWeb.Game.Common;

namespace DominionWeb.Game.Cards.Renaissance
{
    public class Priest : ICard, IAction, IResponseRequired<IEnumerable<Card>>
    {
        public Card Name { get; } = Card.Priest;
        public int Cost { get; } = 4;
        public CardType CardType { get; } = CardType.Action;
        
        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.MoneyPlayed += 2;

            if (player.Hand.Count > 0)
            {
                player.ActionRequest = new SelectCardsActionRequest("Select a card to trash.",
                    Card.Priest, player.Hand, 1);
                player.PlayStatus = PlayStatus.ActionRequestResponder;
            }
            
        }

        public void ResponseReceived(Game game, IEnumerable<Card> cards)
        {            
            var player = game.GetActivePlayer();
            var cardList = cards.ToList();

            if (cardList.Count != 1) return;

            player.TrashFromHand(game.Supply, cardList[0]);
            
            player.PlayStatus = PlayStatus.ActionPhase;

            player.TriggeredAbilities.Add(new PlusTwoMoneyOnTrash());
        }
    }
}