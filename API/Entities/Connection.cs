

namespace API.Entities
{
    public class Connection
    {
        //empty constructor for entity framework to create a new instance of this class
        public Connection()
        {
        }

        public Connection(string connectionId, string username)
        {
            ConnectionId = connectionId;
            Username = username;
        }

        public string ConnectionId { get; set; }

        public string Username { get; set; }
        
    }
}