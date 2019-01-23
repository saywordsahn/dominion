using System;
using System.Collections.Generic;

namespace DominionWeb.Models
{
    public partial class Lobby
    {
        public Lobby()
        {
            LobbyUser = new HashSet<LobbyUser>();
        }

        public int LobbyId { get; set; }
        public string Name { get; set; }
        public string HostId { get; set; }

        public virtual ICollection<LobbyUser> LobbyUser { get; set; }
    }
}
