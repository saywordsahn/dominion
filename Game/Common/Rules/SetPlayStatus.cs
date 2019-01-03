using DominionWeb.Game.Player;

namespace DominionWeb.Game.Common.Rules
{
    public class SetPlayStatus : IRule
    {
        public PlayStatus PlayStatus { get; set; }
        public SetPlayStatus(PlayStatus playStatus)
        {
            PlayStatus = playStatus;
        }
        public void Resolve(Game game, IPlayer player)
        {
            player.PlayStatus = PlayStatus;
            Resolved = true;
        }

        public bool Resolved { get; set; }
    }
}