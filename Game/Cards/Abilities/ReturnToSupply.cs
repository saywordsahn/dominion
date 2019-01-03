using System;
using System.Linq;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class ReturnToSupply : IAbility
    {
        public CardLocation CardLocation { get; set; }
        public Card Card { get; set; }

        public ReturnToSupply(CardLocation cardLocation, Card card)
        {
            CardLocation = cardLocation;
            Card = card;
        }
        
        public void Resolve(Game game, IPlayer player)
        {
            switch (CardLocation)
            {
                case CardLocation.PlayedCards:
                    var playedCard = player.PlayedCards.Last(x => x.Card.Name == Card && x.IsThronedCopy == false);
                    player.PlayedCards.Remove(playedCard);
                    player.GameLog.Add(player.PlayerName.Substring(0, 1) + " returns a " + playedCard.Card.Name);
                    game.Supply.Return(Card);
                    Resolved = true;
                    break;
                default:
                    throw new NotImplementedException("CardLocation " + CardLocation + "is not supported");
            }
        }

        public bool Resolved { get; set; }
    }
}