
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);


        Task<PageList<MemberDto>> GetMembersAsync(UserParams userParams);
        Task<AppUser> GetUserByIdAsync(int id);
        Task<MemberDto> GetUserByMemberNameAsync(string username, bool isCurrentUser);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<string> GetUserGender(string username);
        Task<AppUser> GetUserByPhotoId(int photoId);

    }
}