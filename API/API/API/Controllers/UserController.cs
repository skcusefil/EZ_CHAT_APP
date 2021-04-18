using API.DTOs;
using API.Extensions;
using API.Interface;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAddPhotoService _addPhotoService;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper, IAddPhotoService addPhotoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _addPhotoService = addPhotoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _unitOfWork.UserRepository.GetMembersAsync();

            return Ok(_mapper.Map<IEnumerable<MemberDto>>(users));
        }

        [HttpGet("{username}", Name = "GetUser")]
        public async Task<MemberDto> GetUser(string username)
        {
            return await _unitOfWork.UserRepository.GetMemberByUserNameAsync(username);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto updateUsesr)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(User.GetUsername());

            _mapper.Map(updateUsesr, user);
            _unitOfWork.UserRepository.Update(user);

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to update user");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(username);

            var result = await _addPhotoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            //In future might change to be many photos per user but for this one, 
            //if user upload new photo for profile photo, old photo will be deleted from clound and database
            if(user.Photos.Count > 0)
            {
                var deleteOldPhoto = user.Photos.FirstOrDefault();
                var publicIdToDelete = deleteOldPhoto.PublicId;
                user.Photos.Remove(deleteOldPhoto);
                await _addPhotoService.DeletePhotoAsync(publicIdToDelete);
            }
            
            if(user.Photos.Count == 0)
            {
                photo.IsProfilePhoto = true;
            }


            user.Photos.Add(photo);

            if(await _unitOfWork.Complete())
            {
                return CreatedAtRoute("GetUser", new { username = user.UserName }, _mapper.Map<PhotoDto>(photo));
            }

            return BadRequest("Failed to add photo");
        }

    }
}
