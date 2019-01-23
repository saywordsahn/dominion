using System;
using System.Collections.Generic;

namespace DominionWeb.Models
{
    public partial class Connection
    {
        public int ConnectionId { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
