using API.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public ICollection<FriendInvitation> ReceievedFriendRequests { get; set; }
        public ICollection<FriendInvitation> SentFriendRequests { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public ICollection<Message> MessagesSend { get; set; }
        public ICollection<Message> MessagesRecieve { get; set; }

        [NotMapped]
        public virtual ICollection<FriendInvitation> Friends
        {
            get
            {
                var friends = SentFriendRequests.Where(x => x.FriendStatus == FriendStatus.Approved).ToList();
                friends.AddRange(ReceievedFriendRequests.Where(x => x.FriendStatus == FriendStatus.Approved));
                return friends;
            }
        }
    }
}
