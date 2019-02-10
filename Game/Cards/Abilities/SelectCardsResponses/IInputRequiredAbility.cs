using System;
namespace DominionWeb.Game.Cards.Abilities.SelectCardsResponses
{
    public interface IInputRequiredAbility<T> : IAbility
    {
        T Input { get; set; }
        void SetInput(T input);
    }
}
