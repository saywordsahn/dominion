using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.TriggeredAbilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Utils;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Supply;
using TriggeredAbilityDurationType = DominionWeb.Game.Cards.Abilities.TriggeredAbilities.TriggeredAbilityDurationType;

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
        public int VictoryTokens { get; set; }
        public IActionRequest ActionRequest { get; set; }
        public ICollection<string> GameLog { get; }
        public int DominionCount => Dominion.Count();
        public int VictoryPoints => GetVictoryPointCount();
        public List<ITriggeredAbility> TriggeredAbilities { get; }
        public Stack<PlayedCard> PlayStack { get; set; }
        public IEnumerable<IReaction> RevealedReactions { get; set; }
        public int Coffers { get; set; }
        public int Villagers { get; set; }
        public bool HasBoughtThisTurn { get; set; }
        public Stack<IRule> RuleStack { get; set; }
        public List<IRule> Rules { get; set; }
        public List<Card> PlayedReactions { get; set; }
        public List<Card> Island { get; set; }

        private IEnumerable<Card> Dominion => Deck.Concat(DiscardPile).Concat(Hand).Concat(Island);
         
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
            VictoryTokens = 0;
            Coffers = 0;
            Villagers = 0;
            GameLog = new List<string>();

            PlayedReactions = new List<Card>();
            RuleStack = new Stack<IRule>();
            Rules = new List<IRule>();
            Island = new List<Card>();
        }

        public void Play(Card card)
        {
            Play(CardFactory.Create(card));
        }
        
        public void Play(ICard card)
        {            
            RunTriggeredAbilities(PlayerAction.Play, card.Name);
            Hand.Remove(card.Name);
            PlayedCards.Add(new PlayedCard(card));
            
            switch (card)
            {
                case IAction a:
                    NumberOfActions--;
                    break;
                case ITreasure t:
                    
                    if (t is ITreasureAbilityHolder h)
                    {
                        h.ResolveTreasureAbilities(this);
                    }
                    
                    MoneyPlayed += t.Value;
                    break;
            }
        }
        
        public void PlayWithoutCost(ICard card)
        {
            RunTriggeredAbilities(PlayerAction.Play, card.Name);
            Hand.Remove(card.Name);
            PlayedCards.Add(new PlayedCard(card));
        }

        //this may need to be updated for future triggered abilities
        //this only works for simple triggered abilities
        public void RunTriggeredAbilities(PlayerAction playerAction, Card card)
        {
            foreach (var triggeredAbility in TriggeredAbilities.ToList())
            {
                if (triggeredAbility.Trigger.IsMet(playerAction, card))
                {
                    //triggeredAbility.Ability.Resolve(this);
                    //PlayedAbilities.Add(triggeredAbility.Ability);
                    RuleStack.Push(triggeredAbility.Ability);

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
            RunTriggeredAbilities(PlayerAction.Trash, card);
        }

        public void SetAttacked(Game game)
        {
            //check for duration attack blockers - like champion, lighthouse
            if (PlayedCards.Any(x => x.Card is IAttackBlocker && x.Card is IDuration))
            {
                var attackingPlayer = game.GetAttackingPlayer();
                var attackCard = (IAttack)attackingPlayer.PlayedCards.Last(x => x.Card is IAttack).Card;

                attackCard.AttackNextPlayer(game, this);
            }
            else
            {
                var rule = new RespondToAttackRule();
                RuleStack.Push(rule);
            }
        }

        public int GetVictoryPointCount()
        {
            var vpCount = 0;

            foreach (var card in Dominion)
            {
                var instance = CardFactory.Create(card);

                if (instance is IVictory v)
                {
                    vpCount += v.GetVictoryPointValue(this);
                }
            }
            
            return vpCount + VictoryTokens;
        }

        //TODO: refactor
        public void PlayAllTreasure()
        {
            foreach (var card in Hand.ToList())
            {
                var instance = CardFactory.Create(card);
                
                if (instance is ITreasure t)
                {
                    Play(instance);
//                    RunTriggeredAbilities(PlayerAction.Play, card);
//                    Hand.Remove(card);
//                    PlayedCards.Add(new PlayedCard(instance));
//                    MoneyPlayed += t.Value;
                }
            }
        }

        public void PlayCoffers(int amount)
        {
            if (Coffers < amount) return;

            MoneyPlayed += amount;
            Coffers -= amount;
        }

        public void PlayVillagers(int amount)
        {
            if (Villagers < amount) return;

            NumberOfActions += amount;
            Villagers -= amount;
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

            if (!HasBoughtThisTurn) HasBoughtThisTurn = true;
            
            Gain(card);
            MoneyPlayed -= cardProperties.Cost;
            NumberOfBuys--;
        }

        public void Gain(Card card)
        {
            var instance = CardFactory.Create(card);

            if (instance is IOnGainOverride o)
            {
                o.OnGain(this, card);
            }
            else
            {
                if (instance is IOnGainAbilityHolder ah)
                {
                    ah.ResolveOnGainAbilities(this);
                }
                
                DiscardPile.Add(card); 
            }            
        }
        
        public void Gain(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
            {
                Gain(card);
            }
        }

        public void Discard(Card card)
        {
            GameLog.Add(PlayerName.Substring(0,1) + " discards a " + card.ToString());
            DiscardPile.Add(card);            
        }

        public void GainToHand(Card card)
        {
            var instance = CardFactory.Create(card);

            Hand.Add(card);
           
            if (instance is IOnGainAbilityHolder ah)
            {
                ah.ResolveOnGainAbilities(this);
                //PlayedAbilities.Add(ah.OnGainAbility);
                //OnGainAbilities.Add(ah.OnGainAbility);
            }
        }

        public void GainToHand(Card card, int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                GainToHand(card);
            }
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

        public void StartTurn(Game game)
        {
            foreach (var card in PlayedCards)
            {
                if (card.Card is IDuration d)
                {
                    d.NumberOfTurnsActive++;
                    foreach (var ability in d.GetOnTurnStartAbilities(d.NumberOfTurnsActive))
                    {
                        RuleStack.Push(ability);
                    }
                } 
            }
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
            bool CardIsResolved(PlayedCard x) => !(x.Card is IDuration d && d.Resolved == false);

            DiscardPile.AddRange(PlayedCards.Where(CardIsResolved).Select(x => x.Card.Name));
            PlayedCards.RemoveAll(CardIsResolved);

            DiscardPile.AddRange(Hand);
            TriggeredAbilities.Clear();
            Hand = new List<Card>();
            Draw(5);
            PlayStatus = PlayStatus.WaitForTurn;
            HasBoughtThisTurn = false;
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

        public Card TopCard()
        {
            if (Deck.Count + DiscardPile.Count == 0)
            {
                throw new InvalidOperationException("There are no drawable cards. Call HasDrawableCards before this");
            }
            
            if (Deck.Count > 0)
            {
                return Deck[Deck.Count - 1];
            }
            else
            {
                Shuffle();
                return Deck[Deck.Count - 1];
            }            
        }

        public bool HasDrawableCards() => Deck.Count + DiscardPile.Count > 0;

        public IEnumerable<Card> GetPlayedCardEnums()
        {
            var list = new List<Card>();

            foreach (var card in PlayedCards)
            {
                list.Add(card.Card.Name);
            }
            
            return list;
        }

        public bool HasCardInHand(Card card)
        {
            return Hand.Exists(x => x == card);
        }

        public bool IsRespondingToAbility()
        {
            return PlayStatus == PlayStatus.ActionRequestResponder
                   || PlayStatus == PlayStatus.AttackResponder;
        }

        public bool HasAttackReactionInHand()
        {
            return Hand.Select(x => CardFactory.Create(x))
                .Any(x => x is IAttackReaction);
        }
    }

}
