using System.Collections.Generic;

namespace DominionWeb.Game.Common
{
    public interface ISelectCardsResponseRequired
    {
        void ResponseReceived(Game game, IEnumerable<Card> cards);
    }
}