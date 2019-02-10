using System;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class VassalAbility : IAbility, IResponseRequired<ActionResponse>
    {
        public Card FlippedCard { get; set; }

        public bool Resolved { get; set; }

        public void Resolve(Game game, IPlayer player)
        {
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
            else
            {
                Resolved = true;
            }

        }

        public void ResponseReceived(Game game, ActionResponse response)
        {
            //TODO: create turnPlayer to avoid confusion between playstatuses
            var player = game.GetActivePlayer();

            var flippedCard = CardFactory.Create(FlippedCard);

            if (flippedCard is IAction a)
            {
                player.DiscardPile.RemoveAt(player.DiscardPile.Count - 1);
                player.PlayedCards.Add(new PlayedCard(flippedCard));

                if (flippedCard is IRulesHolder rh)
                {
                    foreach (var rule in rh.GetRules(game, player))
                    {
                        player.RuleStack.Push(rule);
                    }
                }
            }

            player.PlayStatus = PlayStatus.ActionPhase;
            Resolved = true;
        }
    }
}
