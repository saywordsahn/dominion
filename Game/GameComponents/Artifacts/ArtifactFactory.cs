using System;

namespace DominionWeb.Game.GameComponents.Artifacts
{
    public class ArtifactFactory
    {
        public static IArtifact Create(Artifact artifact)
        {
            switch (artifact)
            {
                case Artifact.Flag:
                    return new Flag();
                case Artifact.Horn:
                    return new Horn();
                case Artifact.Key:
                    return new Key();
                case Artifact.Lantern:
                    return new Lantern();
                case Artifact.TreasureChest:
                    return new TreasureChest();
                default:
                    throw new NotImplementedException(artifact + " is not defined in ArtifactFactory.");
            }
        }
    }
}