

using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int sourceUserId, int targetUserId); // get user like
        Task<AppUser> GetUserWithLikes(int userId); // get user with likes
        Task<PageList<LikeDto>> GetUserLikes(LikeParams likeParams); // get user likes
        
    }
}