using API.DTOs;
using API.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserDto>();

            CreateMap<RegisterDto, AppUser>();

            CreateMap<AppUser, MemberDto>()
                 .ForMember(x => x.PhotoUrl, opt => opt
                      .MapFrom(s => s.Photos.FirstOrDefault(x => x.IsProfilePhoto).Url));
            CreateMap<Photo, PhotoDto>();

            CreateMap<MemberUpdateDto, AppUser>();

            CreateMap<Message, MessageDto>()
               .ForMember(dest => dest.SenderPhotoUrl, opt =>
                   opt.MapFrom(s => s.Sender.Photos.FirstOrDefault(x => x.IsProfilePhoto).Url))
               .ForMember(dest => dest.RecipientPhotoUrl, opt =>
                   opt.MapFrom(s => s.Recipient.Photos.FirstOrDefault(x => x.IsProfilePhoto).Url))
                .ForMember(dest => dest.SenderDisplayName, opt =>
                    opt.MapFrom(s => s.Sender.DisplayName))
                .ForMember(dest => dest.RecipientDisplayName, opt => 
                    opt.MapFrom(s => s.Recipient.DisplayName));
        }
    }
}
