using System.Collections.Generic;
using DominionWeb.Game.Cards;

namespace DominionWeb.Game.Player
{
    public class PlayArea : IPlayArea
    {
        public List<ICard> PlayedCards;
        
        public PlayArea()
        {
            PlayedCards = new List<ICard>();
        }

        public IEnumerable<Card> GetCardEnums()
        {
            var list = new List<Card>();

            foreach (var card in PlayedCards)
            {
                list.Add(card.Name);
            }
            
            return list;
        }

        public void Play(Card card)
        {
            PlayedCards.Add(CardFactory.Create(card));
        }

        public void Clear()
        {
            PlayedCards = new List<ICard>();
        }

        public int GetMoneyPlayedAmount()
        {
            int money = 0;
            
            foreach (var card in PlayedCards)
            {
                if (card is ITreasure c)
                {
                    money += c.Value;
                }
            }

            return money;
        }
    }
}