using API.DTOs;
using API.Enums;
using API.Interface;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class FriendRepository : IFriendRepository 
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FriendRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FriendDto>> GetUserFriends()
        {
            var friends = await _context.FriendInvitations.Where(u => u.FriendStatus == FriendStatus.Approved).ToListAsync();

            var listFriendAdd = new List<FriendDto>();

            var friendSent = new List<FriendDto>();
            int i = 0;
            foreach (var friend in friends)
            {
                i++;
                var getFriendInfo = await _context.Users
                    .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(u => u.Id == friend.InvitedUserId);

                friendSent.Add(new FriendDto { Id = i, Friend = getFriendInfo });
            }

            return friendSent;

        }

        public async Task<IEnumerable<FriendDto>> GetRequestsSent(AppUser currentUser)
        {
            var getListSend = await _context.FriendInvitations.Where(sender => sender.SourceUser == currentUser).ToListAsync();

            var listFriendAdd = new List<FriendDto>();

            var friendSent = new List<FriendDto>();
            int i = 0;
            foreach (var sendTo in getListSend)
            {
                i++;
                var getFriendInfo = await _context.Users
                    .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(u => u.Id == sendTo.InvitedUserId);
                friendSent.Add(new FriendDto { Id =i, Friend = getFriendInfo });
            }

            return friendSent;
        }

        public async Task<IEnumerable<FriendDto>> GetRequestsReceived(AppUser currentUser)
        {
            var getListSend = await _context.FriendInvitations.Where(sender => sender.InvitedUser == currentUser).ToListAsync();

            var listFriendAdd = new List<FriendDto>();

            var friendReceived = new List<FriendDto>();
            int i = 0;
            foreach (var receivedFrom in getListSend)
            {
                i++;
                var getFriendInfo = await _context.Users
                    .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(u => u.Id == receivedFrom.SourceUserId);
                friendReceived.Add(new FriendDto { Id = i, Friend = getFriendInfo });
            }

            return friendReceived;
        }

        public void AddFriend(FriendInvitation friendInvitation)
        {
            _context.FriendInvitations.Add(friendInvitation);
        }
        
        public async void UpdateFriendStatus(string status, AppUser requestFrom, AppUser currentUser)
        {
            var friendRequest = await _context.FriendInvitations.Where(user => user.InvitedUser == currentUser && user.SourceUser == requestFrom).FirstOrDefaultAsync();

            switch(status)
            {
                case "Approved": 
                    friendRequest.FriendStatus = FriendStatus.Approved;
                    break;
                case "None": 
                    friendRequest.FriendStatus = FriendStatus.None;
                    break;
                case "Blocked":
                    friendRequest.FriendStatus = FriendStatus.Blocked;
                    break;
                case "Rejected":
                    friendRequest.FriendStatus = FriendStatus.Rejected;
                    break;
                case "Spam":
                    friendRequest.FriendStatus = FriendStatus.Spam;
                    break;
            }


            _context.Entry(friendRequest).State = EntityState.Modified;
        }
    }
}
