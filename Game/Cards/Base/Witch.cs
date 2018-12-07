using System.Collections.Generic;
using System.ComponentModel.Design;
using DominionWeb.Game.Cards.Abilities;

namespace DominionWeb.Game.Cards.Base
{
    //TODO: add attack to cardType or reassess need for cardType on object
    public class Witch : ICard, IAction, IAttack
    {
        private readonly Card _name = Card.Witch;
        private readonly CardType _cardType = CardType.Action;
        private int _cost = 5;
    
        public int Cost => _cost;
        public CardType CardType => _cardType;
        public Card Name => _name;
        
        public IEnumerable<IAbility> Abilities { get; set; }
        public IEnumerable<IActionEvent> ActionEvents { get; set; }

        public void Resolve(Game game)
        {
            var player = game.GetActivePlayer();
            player.PlayStatus = PlayStatus.Attacker;
            
            player.Draw(2);

            game.GetNextPlayer(player).PlayStatus = PlayStatus.Responder;
        }

        public void AttackEffect(IPlayer attackedPlayer, Game game)
        {
            if (!game.Supply.Contains(Card.Curse)) return;
            
            attackedPlayer.Gain(Card.Curse);
            game.Supply.Take(Card.Curse);
        }
    }
}