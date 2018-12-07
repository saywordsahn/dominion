using System.Collections.Generic;

namespace DominionWeb.Game.Player
{
    public interface IPlayArea
    {
        IEnumerable<Card> GetCardEnums();
        void Play(Card card);
        void Clear();
        int GetMoneyPlayedAmount();
    }
}