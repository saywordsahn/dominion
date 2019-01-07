using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.CardSpecificAbilities
{
    public class HideoutAbility : IAbility, IResponseRequired<IEnumerable<Card>>
    {
        public bool Resolved { get; set; }

        public void Resolve(Game game, IPlayer player)
            => new SelectCardFromHand(new NoFilter(), "Select a card to trash.")
                .Resolve(game, player);

        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            var cardList = response.ToList();
            var player = game.GetActivePlayer();

            if (cardList.Count == 1)
            {
                var card = CardFactory.Create(cardList[0]);

                if (card is IVictory)
                {
                    if (game.Supply.Contains(Card.Curse))
                    {
                        game.Supply.Take(Card.Curse);
                        player.Gain(Card.Curse);
                    }
                }

                player.TrashFromHand(game.Supply, card.Name);
                player.PlayStatus = PlayStatus.ActionPhase;
                Resolved = true;
            }
        }
    }
}