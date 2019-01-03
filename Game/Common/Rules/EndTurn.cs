using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Common.Rules
{
    public class EndTurn : IRule
    {
        public void Resolve(Game game, IPlayer player)
        {
            bool CardIsResolved(PlayedCard x) => !(x.Card is IDuration d && d.Resolved == false);

            player.DiscardPile.AddRange(player.PlayedCards.Where(CardIsResolved).Select(x => x.Card.Name));
            player.PlayedCards.RemoveAll(CardIsResolved);

            player.DiscardPile.AddRange(player.Hand);
            player.TriggeredAbilities.Clear();
            player.Hand = new List<Card>();
            player.Draw(5);
            player.HasBoughtThisTurn = false;

            player.RuleStack.Push(new SetPlayStatus(PlayStatus.WaitForTurn));
            
            if (player.OnHandDrawAbilities.Count > 0)
            {
                foreach (var ability in player.OnHandDrawAbilities)
                {
                    player.RuleStack.Push(ability);
                }
            }

            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}