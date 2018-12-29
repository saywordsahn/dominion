using System.Collections.Generic;

namespace DominionWeb.Game.Supply
{
    public interface IPile
    {
        IList<Card> Cards { get; set; }
    }
}