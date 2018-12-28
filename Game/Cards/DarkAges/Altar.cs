using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Common;

namespace DominionWeb.Game.Cards.DarkAges
{
    public class Altar : ICard, IAction, IResponseRequired<IEnumerable<Card>>
    {
        public Card Name { get; } = Card.Altar;
        public int Cost { get; } = 5;
        public CardType CardType { get; } = CardType.Action;
        
        public void Resolve(Game game)
        {
           throw new NotImplementedException();
        }

        public void ResponseReceived(Game game, IEnumerable<Card> response)
        {
            throw new System.NotImplementedException();
        }
    }
}