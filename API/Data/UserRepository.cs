

using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

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
        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id); // FindAsync() is a method that is available to us from the DbContext class.
        }

        public async Task<MemberDto> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.Include(p => p.Photos)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.UserName == username); // SingleOrDefaultAsync() is a method that is available to us from the DbContext class.
        }

        public async Task<IEnumerable<MemberDto>> GetUsersAsync()
        {
            return await _context.Users.Include(p => p.Photos)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .ToListAsync(); // ToListAsync() is a method that is available to us from the DbContext class.
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0; // SaveChangesAsync() is a method that is available to us from the DbContext class.
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified; // Update the user in the database.
        }
    }
}