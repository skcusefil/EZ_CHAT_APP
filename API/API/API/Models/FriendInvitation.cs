using API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class FriendInvitation
    {
        public int SourceUserId { get; set; }
        public int InvitedUserId { get; set; }
        public AppUser SourceUser { get; set; }  //Who send invitation
        public AppUser InvitedUser { get; set; } //Who recieve invitation
        public FriendStatus FriendStatus { get; set; }
    }
}
