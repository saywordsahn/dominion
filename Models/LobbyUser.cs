using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DominionWeb.Models
{
    public partial class LobbyUser
    {
        public int Id { get; set; }
        public int LobbyId { get; set; }
        public string UserId { get; set; }

        [JsonIgnore]
        public virtual Lobby Lobby { get; set; }
    }
}
