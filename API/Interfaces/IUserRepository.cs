
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync(); // Return a boolean to indicate whether or not the save was successful.
        Task<IEnumerable<MemberDto>> GetUsersAsync(); // Return a list of users.
        Task<AppUser> GetUserByIdAsync(int id); // Return a single user.
        Task<MemberDto> GetUserByMemberNameAsync(string username); // Return a single user.
        Task<AppUser> GetUserByUsernameAsync(string username); // Return a single user.
        
    }
}