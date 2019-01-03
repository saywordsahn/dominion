using System.Collections.Generic;

namespace DominionWeb.Game.Supply
{
    public interface IPile
    {
        Card PileCard { get; set; }
        IList<Card> Cards { get; set; }
    }
}