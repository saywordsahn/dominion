using System;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.SelectCardsResponses
{
    public class AddTo : IAbility, IInputRequiredAbility<ICard>
    {
        public AddTo()
        {
        }

        public bool Resolved { get; set; }

        public ICard Input { get; set; }

        public void Resolve(Game game, IPlayer player)
        {
            if (Input == null) throw new InvalidOperationException("Input is required");

            //player.DiscardPile.Remove(card);
            player.Deck.Add(Input.Name);
            player.PlayStatus = PlayStatus.ActionPhase;
            Resolved = true;
        }

        public void SetInput(ICard input)
        {
            Input = input;
        }
    }
}
