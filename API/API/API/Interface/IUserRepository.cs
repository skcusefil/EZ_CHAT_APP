using API.DTOs;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<AppUser> GetUserByIdAnsync(int id);
        Task<AppUser> GetUserByUsername(string username);
        Task<AppUser> GetUserByUserNameAsync(string username);
        Task<MemberDto> GetMemberByUserNameAsync(string username);
        void Update(AppUser user);
    }
}
