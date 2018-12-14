using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Utils;
using DominionWeb.Game.Common;
using DominionWeb.Game.Supply;

namespace DominionWeb.Game.Player
{
    public class Player : IPlayer
    {        
        public int PlayerId { get; }
        public string PlayerName { get; }
        public List<Card> Hand { get; private set; }
        public List<Card> Deck { get; private set; }
        public List<Card> DiscardPile { get; private set; }
        public List<PlayedCard> PlayedCards { get; private set; }
        public PlayStatus PlayStatus { get; set; }
        public int MoneyPlayed { get; set; }
        public int NumberOfBuys { get; set; }
        public int NumberOfActions { get; set; }
        public IActionRequest ActionRequest { get; set; }
        public ICollection<string> GameLog { get; }
        public int DominionCount => Dominion.Count();
        public int VictoryPoints => GetVictoryPointCount();
        public List<ITriggeredAbility> TriggeredAbilities { get; }
        public Stack<PlayedCard> PlayStack { get; set; }
        
        private IEnumerable<Card> Dominion => Deck.Concat(DiscardPile).Concat(Hand);
         
        public Player(int id, string playerName)
        {
            PlayerId = id;
            PlayerName = playerName;
            Hand = new List<Card>();
            Deck = new List<Card>();
            DiscardPile = new List<Card>();
            PlayedCards = new List<PlayedCard>();
            PlayStack = new Stack<PlayedCard>();
            TriggeredAbilities = new List<ITriggeredAbility>();
            PlayStatus = PlayStatus.GameStart;
            MoneyPlayed = 0;
            NumberOfBuys = 0;
            NumberOfActions = 0;
            GameLog = new List<string>();
        }

        public void Play(Card card)
        {
            Play(CardFactory.Create(card));
        }
        
        public void Play(ICard card)
        {            
            RunTriggeredAbilities(card.Name);
            Hand.Remove(card.Name);
            PlayedCards.Add(new PlayedCard(card));
            
            switch (card)
            {
                case IAction a:
                    NumberOfActions--;
                    break;
                case ITreasure t:
                    MoneyPlayed += t.Value;
                    break;
            }
        }
        
        public void PlayWithoutCost(ICard card)
        {
            RunTriggeredAbilities(card.Name);
            Hand.Remove(card.Name);
            PlayedCards.Add(new PlayedCard(card));
        }

        //this may need to be updated for future triggered abilities
        //for now -- YAGNI
        private void RunTriggeredAbilities(Card card)
        {
            foreach (var triggeredAbility in TriggeredAbilities.ToList())
            {
                if (triggeredAbility.Trigger.IsMet(PlayerAction.Play, card))
                {
                    triggeredAbility.Ability.Resolve(this);

                    if (triggeredAbility.TriggeredAbilityDurationType == TriggeredAbilityDurationType.Once)
                    {
                        TriggeredAbilities.Remove(triggeredAbility);
                    }
                }
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

        //TODO: refactor
        public void PlayAllTreasure()
        {
            foreach (var card in Hand.ToList())
            {
                var instance = CardFactory.Create(card);
                
                if (instance is ITreasure t)
                {
                    RunTriggeredAbilities(card);
                    Hand.Remove(card);
                    PlayedCards.Add(new PlayedCard(instance));
                    MoneyPlayed += t.Value;
                }
            }
        }

        public ICard GetLastPlayedCard()
        {
            return PlayedCards[PlayedCards.Count - 1].Card;
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

        public void DiscardFromHand(Card card)
        {
            Hand.Remove(card);
            DiscardPile.Add(card);
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
            TriggeredAbilities.Clear();
            PlayedCards = new List<PlayedCard>();
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
                list.Add(card.Card.Name);
            }
            
            return list;
        }
        
    }

}
