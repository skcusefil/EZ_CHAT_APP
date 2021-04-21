using System;
using System.Collections.Generic;
using System.Text;

namespace clientXamarin.Models
{
    class Group
    {
        public string Name { get; set; } 
        public ICollection<Connection> Connections { get; set; } = new List<Connection>();
    }

    class Connection
    {
        public string ConnectionId { get; set; }
        public string Username { get; set; }
    }
}
