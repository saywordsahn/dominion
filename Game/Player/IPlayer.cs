using System;
using System.Collections.Generic;
using DominionWeb.Game.Cards;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Common;
using DominionWeb.Game.Supply;

namespace DominionWeb.Game
{
    public interface IPlayer
    {
        void Play(ICard card);
        void Play(Card card);
        void Gain(Card card);
        void Gain(IEnumerable<Card> cards);
        void GainToHand(Card card);
        void Shuffle();
        void Draw(int numberToDraw);
        int MoneyPlayed { get; set; }
        int NumberOfBuys { get; set; }
        PlayStatus PlayStatus { get; set; }
        string PlayerName { get; }
        int NumberOfActions { get; set; }
        void ClearPlayedCards();
        void Buy(Card card);
        void EndTurn();
        void StartTurn();
        void EndActionPhase();
        ICard GetLastPlayedCard();
        bool HasActionInHand();
        void PlayAllTreasure();
        List<Card> Deck { get; }
        List<Card> DiscardPile { get; }
        List<ICard> PlayedCards { get; }
        List<Card> Hand { get; }
        List<ITriggeredAbility> TriggeredAbilities { get; }
        ICollection<string> GameLog { get; }
        IActionRequest ActionRequest { get; set; }
        void Discard(Card card);
        void TrashFromHand(ISupply supply, Card card);
        int DominionCount { get; }
    }
}
