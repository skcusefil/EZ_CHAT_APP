using API.DTOs;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interface
{
    public interface IFriendRepository
    {
        void AddFriend(FriendInvitation friendInvitation);
        Task<IEnumerable<FriendDto>> GetFriends();
        Task<IEnumerable<FriendDto>> GetRequestsSent(AppUser currentUser);
        Task<IEnumerable<FriendDto>> GetRequestsReceived(AppUser currentUser);
    }
}
