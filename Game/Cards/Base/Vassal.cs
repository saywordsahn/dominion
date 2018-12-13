using System;
using DominionWeb.Game.Common;

namespace DominionWeb.Game.Cards.Base
{
    public class Vassal : ICard, IAction, IActionRequester
    {
        public int Cost { get; } = 3;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Vassal;
        
        public Card FlippedCard { get; set; }

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.MoneyPlayed += 2;
            
            //TODO: code unhappy path
            var topCard = player.Deck[player.Deck.Count - 1];
            player.Deck.RemoveAt(player.Deck.Count - 1);
            player.Discard(topCard);

            FlippedCard = topCard;

            var instance = CardFactory.Create(topCard);

            if (instance is IAction)
            {
                player.ActionRequest = new YesNoActionRequest(Card.Vassal, string.Concat("Would you like to play ", topCard.ToString()));
                player.PlayStatus = PlayStatus.ActionRequestResponder;
            }
            
        }

        public void ResponseReceived(Game game, ActionResponse response)
        {
            //TODO: create turnPlayer to avoid confusion between playstatuses
            var player = game.GetActivePlayer();
            player.PlayStatus = PlayStatus.ActionPhase;

            var flippedCard = CardFactory.Create(FlippedCard);

            if (flippedCard is IAction a)
            {
                player.DiscardPile.RemoveAt(player.DiscardPile.Count - 1);
                player.PlayedCards.Add(new PlayedCard(flippedCard));
                player.PlayStack.Push(new PlayedCard(flippedCard, false));
            }

        }

        
    }
}