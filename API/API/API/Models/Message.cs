using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string UserMessage { get; set; }
        public User Sender { get; set; }
        public User Reciever { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
