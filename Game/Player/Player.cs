using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DominionWeb.Game.Cards;
using DominionWeb.Game.Cards.Base;
using DominionWeb.Game.Utils;
using DominionWeb.Game.Common;
using DominionWeb.Game.Supply;
using Microsoft.AspNetCore.Http;

namespace DominionWeb.Game.Player
{
    public class Player : IPlayer
    {        
        public int PlayerId { get; private set; }
        public string PlayerName { get; private set; }
        public List<Card> Hand { get; private set; }
        public List<Card> Deck { get; private set; }
        public List<Card> DiscardPile { get; private set; }
        public List<ICard> PlayedCards { get; private set; }
        public PlayStatus PlayStatus { get; set; }
        public int MoneyPlayed { get; set; }
        public int NumberOfBuys { get; set; }
        public int NumberOfActions { get; set; }
        public IActionRequest ActionRequest { get; set; }
        public ICollection<string> GameLog { get; private set; }
        public int DominionCount => Dominion.Count();
        public int VictoryPoints => GetVictoryPointCount();
        
        private IEnumerable<Card> Dominion => Deck.Concat(DiscardPile).Concat(Hand);
        
         
        public Player(int id, string playerName)
        {
            PlayerId = id;
            PlayerName = playerName;
            Hand = new List<Card>();
            Deck = new List<Card>();
            DiscardPile = new List<Card>();
            PlayedCards = new List<ICard>();
            PlayStatus = PlayStatus.GameStart;
            MoneyPlayed = 0;
            NumberOfBuys = 0;
            NumberOfActions = 0;
            GameLog = new List<string>();
        }

        public void Play(Card card)
        {
            if (!Hand.Contains(card)) return;
           
            var instance = CardFactory.Create(card);

            if (PlayStatus == PlayStatus.ActionPhase && instance is IAction a)
            {
                Hand.Remove(card);
                PlayedCards.Add(instance);
            }
            else if (PlayStatus == PlayStatus.BuyPhase && instance is ITreasure t)
            {
                Hand.Remove(card);
                PlayedCards.Add(instance);
                MoneyPlayed += t.Value;
            }
        }

        public void TrashFromHand(ISupply supply, Card card)
        {
            var cardInHand = Hand.First(x => x == card);
            Hand.Remove(cardInHand);
            GameLog.Add(PlayerName.Substring(0, 1) + " trashes a " + card.ToString());
            supply.AddToTrash(card);
        }

        public int GetVictoryPointCount()
        {
            var vpCount = 0;

            foreach (var card in this.Dominion)
            {
                var instance = CardFactory.Create(card);

                if (instance is IVictory v)
                {
                    vpCount += v.GetVictoryPointValue(this);
                }
            }

            return vpCount;
        }

        public void PlayAllTreasure()
        {
            foreach (var card in Hand.ToList())
            {
                var instance = CardFactory.Create(card);
                
                if (instance is ITreasure t)
                {
                    Hand.Remove(card);
                    PlayedCards.Add(instance);
                    MoneyPlayed += t.Value;
                }
            }
        }

        public ICard GetLastPlayedCard()
        {
            return PlayedCards[PlayedCards.Count - 1];
        }

        public void Buy(Card card)
        {
            if (NumberOfBuys == 0) throw new InvalidOperationException("Cannot purchase card: no buys remaining.");
            
            var cardProperties = CardFactory.Create(card);

            if (cardProperties.Cost > MoneyPlayed) throw new InvalidOperationException("Cannot purchase card: not enough money.");
            
            Gain(card);
            MoneyPlayed -= cardProperties.Cost;
            NumberOfBuys--;
        }

        public void Gain(Card card)
        {
            DiscardPile.Add(card);
        }
        
        public void Gain(IEnumerable<Card> cards)
        {
            DiscardPile.AddRange(cards);
        }

        public void Discard(Card card)
        {
            GameLog.Add(PlayerName.Substring(0,1) + " discards a " + card.ToString());
            DiscardPile.Add(card);            
        }

        public void GainToHand(Card card)
        {
            Hand.Add(card);
        }
        
        public void Shuffle()
        {
            DiscardPile.Shuffle();
            Deck = DiscardPile;
            DiscardPile = new List<Card>();
        }

        public void StartTurn()
        {
            NumberOfActions = 1;
            NumberOfBuys = 1;
            MoneyPlayed = 0;
            
            PlayStatus = HasActionInHand() ? PlayStatus.ActionPhase : PlayStatus.BuyPhase;
            
        }

        public bool HasActionInHand()
        {
            foreach (var card in Hand)
            {
                var instance = CardFactory.Create(card);

                if (instance is IAction)
                {
                    return true;
                }
            }

            return false;
        }
        
        public void EndTurn()
        {
            DiscardPile.AddRange(GetPlayedCardEnums());
            DiscardPile.AddRange(Hand);
            PlayedCards = new List<ICard>();
            Hand = new List<Card>();
            Draw(5);
            PlayStatus = PlayStatus.WaitForTurn;
        }

        public void EndActionPhase()
        {
            PlayStatus = PlayStatus.BuyPhase;
        }
        
        public void Draw(int numberToDraw)
        {
            var drawn = new List<Card>();

            while (Deck.Count > 0 && drawn.Count < numberToDraw) {
                var topCard = Deck[Deck.Count - 1];
                Deck.RemoveAt(Deck.Count - 1);
                drawn.Add(topCard);
            }
            
            Hand.AddRange(drawn);

            numberToDraw = numberToDraw - drawn.Count;

            if (DiscardPile.Count > 0 && numberToDraw > 0 && Deck.Count == 0)
            {
                Shuffle();
                drawn = new List<Card>();
                
                while (Deck.Count > 0 && drawn.Count < numberToDraw) {
                    var topCard = Deck[Deck.Count - 1];
                    Deck.RemoveAt(Deck.Count - 1);
                    drawn.Add(topCard);
                }
                
                Hand.AddRange(drawn);
            }
        }

        public IEnumerable<Card> GetPlayedCardEnums()
        {
            var list = new List<Card>();

            foreach (var card in PlayedCards)
            {
                list.Add(card.Name);
            }
            
            return list;
        }

        public void ClearPlayedCards()
        {
            PlayedCards = new List<ICard>();
        }
        
    }

}
