using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Common;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class IslandAbility : IAbility, IResponseRequired<IEnumerable<Card>>
    {
        public bool Resolved { get; set; }

        public void Resolve(Game game, IPlayer player)
            => new SelectCardFromHand("Select a card to place on your island mat.")
                .Resolve(game, player);


        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var cardList = response.ToList();

            if (cardList.Count != 1) return;

            var player = game.GetActivePlayer();

            var selectedCard = cardList[0];
            player.Hand.Remove(selectedCard);
            var islandCard = player.PlayedCards.First(x => x.Card.Name == Card.Island);
            player.PlayedCards.Remove(islandCard);
            player.Island.Add(Card.Island);
            player.Island.Add(selectedCard);

            player.PlayStatus = PlayStatus.ActionPhase;
            Resolved = true;
        }
    }
}