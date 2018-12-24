using System.Linq;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.AttackReactions;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.DarkAges
{
    public class Beggar : ICard, IAction, IReaction, IAttackReaction, IRuleHolder
    {
        public Card Name { get; } = Card.Beggar;
        public int Cost { get; } = 2;
        public CardType CardType { get; } = CardType.Action;
        
        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.GainToHand(Card.Copper, 3);
        }

        public IRule ReactionEffect() => new DiscardCardForTwoSilvers();

        public IRule GetRule(Game game, IPlayer player)
        {
            return new GainCardToHand(Card.Copper, 3);
        }
    }
}