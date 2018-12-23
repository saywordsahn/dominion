using System.Linq;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.AttackReactions;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Rules;

namespace DominionWeb.Game.Cards.DarkAges
{
    public class Beggar : ICard, IAction, IReaction, IAttackReaction, IResponseRequired<ActionResponse>
    {
        public Card Name { get; } = Card.Beggar;
        public int Cost { get; } = 2;
        public CardType CardType { get; } = CardType.Action;
        public PlayStatus PreviousStatus { get; set; }
        
        public void Resolve(Game game)
        {
            throw new System.NotImplementedException();
        }

        public void ReactionEffect(Game game)
        {
            var player = game.GetActivePlayer();
            PreviousStatus = player.PlayStatus;
            player.ActionRequest = new YesNoActionRequest(Card.Beggar, "Discard Beggar to gain two silvers?");
            player.PlayStatus = PlayStatus.ActionRequestResponder;
        }

        public void ResponseReceived(Game game, ActionResponse response)
        {
            //TODO: code unhappy path
            //TODO: check rule and perhaps code GainToDeck
            var player = game.GetActivePlayer();

            if (response == ActionResponse.Yes)
            {
                player.Gain(Card.Silver);
                player.Gain(Card.Silver);
                game.Supply.Take(Card.Silver);
                game.Supply.Take(Card.Silver);
                var lastSilver = player.DiscardPile.Last();
                player.DiscardPile.RemoveAt(player.DiscardPile.Count - 1);
                player.Deck.Add(lastSilver);
            }

            player.PlayStatus = PreviousStatus;

        }

        public IRule ReactionEffect() => new DiscardCardForTwoSilvers();
    }
}