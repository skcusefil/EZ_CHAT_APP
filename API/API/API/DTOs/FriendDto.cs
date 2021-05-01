using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class FriendDto
    {
        public int Id { get; set; }
        public MemberDto Friend { get; set; } 
    }
}
