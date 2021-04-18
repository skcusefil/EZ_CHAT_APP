using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public AppUser Sender { get; set; }
        public AppUser Reciever { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
