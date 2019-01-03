using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Player;
using Microsoft.AspNetCore.Rewrite;

namespace DominionWeb.Game.GameComponents.Artifacts
{
    public class Flag : IArtifact, IOnHandDrawAbilityHolder
    {
        public void OnHandDraw(IPlayer player)
        {
            player.OnHandDrawAbilities.Add(new PlusCards(1));
        }

        public Artifact Artifact { get; set; } = Artifact.Flag;
    }
}