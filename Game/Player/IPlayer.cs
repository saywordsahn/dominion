using System;
using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Abilities.Attacks.Effects;
using DominionWeb.Game.Cards.Abilities.TriggeredAbilities;
using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.GameComponents.Artifacts;
using DominionWeb.Game.Supply;

namespace DominionWeb.Game.Player
{
    public interface IPlayer
    {
        void Play(ICard card);
        void Play(Card card);
        void Gain(Card card);
        void Gain(ICard card);
        void Gain(IEnumerable<Card> cards);
        void GainToHand(Card card);
        void GainToHand(Card card, int amount);
        void DiscardFromHand(Card card);
        void Shuffle();
        void Draw(int numberToDraw);
        int MoneyPlayed { get; set; }
        int NumberOfBuys { get; set; }
        PlayStatus PlayStatus { get; set; }
        string PlayerName { get; }
        int NumberOfActions { get; set; }
        int VictoryTokens { get; set; }
        void Buy(Card card);
        void EndTurn();
        void StartTurn(Game game);
        void EndActionPhase();
        ICard GetLastPlayedCard();
        bool HasActionInHand();
        void PlayAllTreasure();
        List<Card> Deck { get; }
        List<Card> DiscardPile { get; }
        List<PlayedCard> PlayedCards { get; }
        List<Card> Hand { get; set; }
        List<ITriggeredAbility> TriggeredAbilities { get; }
        ICollection<string> GameLog { get; }
        IActionRequest ActionRequest { get; set; }
        void Discard(Card card);
        void TrashFromHand(ISupply supply, Card card);
        int DominionCount { get; }
        void PlayWithoutCost(ICard instance);
        int Coffers { get; set; }
        int Villagers { get; set; }
        bool HasCardInHand(Card card);
        bool IsRespondingToAbility();
        bool HasAttackReactionInHand();
        void SetAttacked(Game game, IAttackEffect attackEffect);
        void RunTriggeredAbilities(PlayerAction playerAction, Card card);
        Stack<IRule> RuleStack { get; set; }
        List<IRule> Rules { get; set; }
        List<Card> PlayedReactions { get; set; }
        List<Card> Island { get; set; }
        Card TopCard();
        bool HasDrawableCards();
        bool HasBoughtThisTurn { get; set; }
        void PlayCoffers(int amount);
        void PlayVillagers(int amount);
        int GetCardCount(ICardFilter filter);
        TavernMat TavernMat { get; set; }
        ExileMat ExileMat { get; set; }
        bool JourneyTokenIsFaceUp { get; set; }
        IEnumerable<Card> GetTopCards(int number);

        List<IArtifact> Artifacts { get; set; }
        List<IAbility> OnHandDrawAbilities { get; set; }

    }
}
