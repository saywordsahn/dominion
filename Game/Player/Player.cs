using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.Attacks.Effects;
using DominionWeb.Game.Cards.Abilities.TriggeredAbilities;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Utils;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.GameComponents.Artifacts;
using DominionWeb.Game.Supply;
using IAction = DominionWeb.Game.Cards.Types.IAction;
using IAttackReaction = DominionWeb.Game.Cards.Types.IAttackReaction;
using ICard = DominionWeb.Game.Cards.Types.ICard;
using IDuration = DominionWeb.Game.Cards.Types.IDuration;
using IOnGainAbilityHolder = DominionWeb.Game.Cards.Types.IOnGainAbilityHolder;
using IOnGainOverride = DominionWeb.Game.Cards.Types.IOnGainOverride;
using ITreasure = DominionWeb.Game.Cards.Types.ITreasure;
using ITreasureAbilityHolder = DominionWeb.Game.Cards.Types.ITreasureAbilityHolder;
using IVictory = DominionWeb.Game.Cards.Types.IVictory;

namespace DominionWeb.Game.Player
{
    public class Player : IPlayer
    {        
        public int PlayerId { get; }
        public string PlayerName { get; }
        public List<Card> Hand { get; set; }
        public List<Card> Deck { get; set; }
        public List<Card> DiscardPile { get; set; }
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
        public List<IAbility> OnHandDrawAbilities { get; set; }

        public List<IArtifact> Artifacts { get; set; }
        public TavernMat TavernMat { get; set; }

        public bool JourneyTokenIsFaceUp { get; set; }

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
            Artifacts = new List<IArtifact>();
            OnHandDrawAbilities = new List<IAbility>();
            TavernMat = new TavernMat();
            JourneyTokenIsFaceUp = true;
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
                    
                    if (triggeredAbility.TriggeredAbilityDurationType != TriggeredAbilityDurationType.Once)
                    {
                        triggeredAbility.Ability.Resolved = false;
                    }
                    
                    RuleStack.Push(triggeredAbility.Ability);

                    //we may not need this since resolved abilities won't play 2 times anyway
//                    if (triggeredAbility.TriggeredAbilityDurationType == TriggeredAbilityDurationType.Once)
//                    {
//                        TriggeredAbilities.Remove(triggeredAbility);
//                    }
                }
            }
        }

        public void TrashFromHand(ISupply supply, Card card)
        {
            var cardInHand = Hand.First(x => x == card);
            var instance = CardFactory.Create(cardInHand);
            Hand.Remove(cardInHand);
            GameLog.Add(PlayerName.Substring(0, 1) + " trashes a " + card);
            supply.AddToTrash(card);

            if (instance is IOnTrashAbilityHolder abilityHolder)
            {
                abilityHolder.ResolveOnTrashAbilities(this);
            }
            
            RunTriggeredAbilities(PlayerAction.Trash, card);
        }

        public void SetAttacked(Game game, IAttackEffect attackEffect)
        {
            RuleStack.Push(new RespondToAttackRule(attackEffect));
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
                else if (instance is ICurse c)
                {
                    vpCount += c.GetVictoryPointValue(this);
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
            
            var instance = CardFactory.Create(card);

            if (instance.Cost > MoneyPlayed) throw new InvalidOperationException("Cannot purchase card: not enough money.");

            if (!HasBoughtThisTurn) HasBoughtThisTurn = true;
            
            Gain(card);
            MoneyPlayed -= instance.Cost;
            NumberOfBuys--;

            RunTriggeredAbilities(PlayerAction.Buy, card);
            
            if (instance is IOnBuyAbilityHolder b)
            {
                //TODO, rework?
                b.ResolveOnGainAbilities(this);
            }
        }

        public void Gain(Card card)
        {
            var instance = CardFactory.Create(card);
            Gain(instance);
        }

        public void Gain(ICard card)
        {
            if (card is IOnGainOverride o)
            {
                o.OnGain(this, card.Name);
            }
            else
            {
                if (card is IOnGainAbilityHolder ah)
                {
                    ah.ResolveOnGainAbilities(this);
                }
                
                DiscardPile.Add(card.Name); 
            }     
            
            //check on gain triggers
            RunTriggeredAbilities(PlayerAction.Gain, card.Name);
           
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
           RuleStack.Push(new EndTurn());
        }

        public void EndActionPhase()
        {
            PlayStatus = PlayStatus.BuyPhase;
        }
        
        public IEnumerable<Card> GetTopCards(int number)
        {
            if (Deck.Count < number)
            {
                var topCards = Deck;
                Shuffle();
                Deck.AddRange(topCards);
            }
            
            return Deck.TakeLast(number);
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

        public int GetCardCount(ICardFilter filter)
        {
            return Dominion.Select(CardFactory.Create)
                .Count(filter.Apply);
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
