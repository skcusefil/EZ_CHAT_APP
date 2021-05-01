using API.DTOs;
using API.Enums;
using API.Extensions;
using API.Interface;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class FriendInvitationController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FriendInvitationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("friends")]
        public async Task<ActionResult<IEnumerable<FriendDto>>> GetUserFriends()
        {
            var friends = await _unitOfWork.FriendRepository.GetUserFriends();

            return Ok(friends);
        }

        [HttpGet("sent-request")]
        public async Task<ActionResult<IEnumerable<FriendDto>>> GetRequestsSent()
        {
            var currentUser = await _unitOfWork.UserRepository.GetUserByUsername(User.GetUsername());
            var friends = await _unitOfWork.FriendRepository.GetRequestsSent(currentUser);

            return Ok(friends);
        }

        [HttpGet("received-request")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetRequestsReceived()
        {
            var currentUser = await _unitOfWork.UserRepository.GetUserByUsername(User.GetUsername());
            var friends = await _unitOfWork.FriendRepository.GetRequestsReceived(currentUser);

            return Ok(friends);
        }


        [HttpPost("add-friend")]
        public async Task<ActionResult> AddFriend(string friendUsername)
        {
            var friend = await _unitOfWork.UserRepository.GetUserByUsername(friendUsername);
            var user = await _unitOfWork.UserRepository.GetUserByUsername(User.GetUsername());

            var friendRequest = new FriendInvitation
            {
                SourceUser = user,
                InvitedUser = friend,
                FriendStatus = FriendStatus.None
            };

            _unitOfWork.FriendRepository.AddFriend(friendRequest);

            if (await _unitOfWork.Complete()) return Ok("Friend added");

            return BadRequest("Failed to add friend");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateFriendStatus(string status, string receivedFrom)
        {
            var currentUser = await _unitOfWork.UserRepository.GetUserByUsername(User.GetUsername());
            var requestFrom = await _unitOfWork.UserRepository.GetUserByUsername(receivedFrom);

            _unitOfWork.FriendRepository.UpdateFriendStatus(status, requestFrom, currentUser);

            if (await _unitOfWork.Complete()) return Ok(requestFrom.DisplayName +" is now friend with "+ currentUser.DisplayName);

            return BadRequest("Failed to add friend");
        }

    }
}
