using System.Collections.Generic;

namespace DominionWeb.Models
{
    public partial class User
    {
        public User()
        {
            Connection = new HashSet<Connection>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }

        public ICollection<Connection> Connection { get; set; }
    }
}
