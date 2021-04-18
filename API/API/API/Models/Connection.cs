using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Connection
    {
        public Connection(string connectionId, string username)
        {
            this.ConnectionId = connectionId;
            this.Username = username;

        }
        public string ConnectionId { get; set; }
        public string Username { get; set; }
    }
}
