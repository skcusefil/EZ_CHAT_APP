using API.DTOs;
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
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public Task<AppUser> GetUserByIdAnsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetUserByUserNameAsync(string username)
        {
            return await _context.Users
           .Include(p => p.Photos)
           .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            return await _context.Users.Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<MemberDto> GetMemberByUserNameAsync(string username)
        {
            return await _context.Users
                           .Where(x => x.UserName == username)
                           .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                           .SingleOrDefaultAsync();
        }
    }
}
