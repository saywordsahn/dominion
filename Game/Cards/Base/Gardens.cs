using System;

namespace DominionWeb.Game.Cards.Base
{
    public class Gardens : ICard, IVictory
    {
        public int Cost { get; } = 4;

        public CardType CardType { get; } = CardType.Victory;

        public Card Name { get; } = Card.Gardens;
        
        public int GetVictoryPointValue(IPlayer player)
        {
            var dominionSize = player.GetDominionCount();

            return dominionSize / 10;
        }
    }
}