using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.TriggeredAbilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Renaissance
{
    public class Priest : ICard, IAction, IResponseRequired<IEnumerable<Card>>
    {
        public Card Name { get; } = Card.Priest;
        public int Cost { get; } = 4;
        public CardType CardType { get; } = CardType.Action;

        //TODO: Implement
        public IEnumerable<IRule> GetRules(Game game, IPlayer player)
        {
            throw new System.NotImplementedException();
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