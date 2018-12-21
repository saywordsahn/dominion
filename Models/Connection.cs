namespace DominionWeb.Models
{
    public partial class Connection
    {
        public int ConnectionId { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
