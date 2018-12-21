using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Base
{
    //TODO: add attack to cardType or reassess need for cardType on object
    public class Witch : ICard, IAction, IAttack
    {
        public int Cost { get; } = 5;

        public CardType CardType { get; } = CardType.Action;

        public Card Name { get; } = Card.Witch;

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