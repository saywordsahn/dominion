using System.Linq;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.GameComponents.Artifacts;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities
{
    public class GainArtifact: IAbility
    {
        public Artifact Artifact { get; set; }
        public bool Resolved { get; set; }

        public GainArtifact(Artifact artifact)
        {
            Artifact = artifact;
        }
        
        public void Resolve(Game game, IPlayer player)
        {

            if (player.Artifacts.Exists(x => x.Artifact == Artifact))
            {
                Resolved = true;
                return;
            }

            var playerWithArtifact = game.Players
                .Where(p => p != player)
                .SingleOrDefault(x => x.Artifacts.SingleOrDefault(a => a.Artifact == Artifact) != null);

            IArtifact artifact;
            
            if (playerWithArtifact == null)
            {
                artifact = ArtifactFactory.Create(Artifact);
                player.Artifacts.Add(artifact);
            }
            else
            {
                artifact = playerWithArtifact.Artifacts.SingleOrDefault(x => x.Artifact == Artifact);
                playerWithArtifact.Artifacts.Remove(artifact);
                
                player.Artifacts.Add(artifact);
            }
            
            //handle actions here
            if (artifact is IOnHandDrawAbilityHolder ah)
            {
                ah.OnHandDraw(player);
                //TODO: must remove ability from player artifact was taken from
                //var oldArtifactAbility = playerWithArtifact.OnHandDrawAbilities.Where(x => x.)
                //playerWithArtifact.OnHandDrawAbilities.Remove
            }

            Resolved = true;
        }
    }
}