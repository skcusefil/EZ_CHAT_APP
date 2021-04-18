using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AppUser : IdentityUser<int>
    {
        //public int Id { get; set; }
        //public string UserName { get; set; }

        //public byte[] PasswordHash { get; set; }

        //public byte[] PasswordSalt { get; set; }
        public string DisplayName { get; set; }

        public ICollection<FriendInvitation> InvitedBy { get; set; }
        public ICollection<FriendInvitation> InvitedFrom { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public ICollection<Message> MessagesSend { get; set; }
        public ICollection<Message> MessagesRecieve { get; set; }
    }
}
