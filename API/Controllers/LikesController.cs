
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{

    public class LikesController : BaseApiController
    {
        private readonly IUserRepository _userRepository;

        private readonly ILikesRepository _likesRepository;

        public LikesController(IUserRepository userRepository, ILikesRepository likesRepository)
        {
            _userRepository = userRepository;

            _likesRepository = likesRepository;
        }
        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var sourceUserId = User.GetUserId(); // get user id
            var likedUser = await _userRepository.GetUserByUsernameAsync(username); // get user by username
            var sourceUser = await _likesRepository.GetUserWithLikes(sourceUserId); // get user with likes
            if (likedUser == null) return NotFound(); // if user is not found return not found
            if (sourceUser.UserName == username) return BadRequest("You cannot like yourself"); // if user is liked by himself return bad request
            var userLike = await _likesRepository.GetUserLike(sourceUserId, likedUser.Id); // get user like
            if (userLike != null) return BadRequest("You already like this user"); // if user is already liked return bad request
            userLike = new UserLike // create new user like
            {
                SourceUserId = sourceUserId, // source user id
                TargetUserId = likedUser.Id // liked user id
            };
            sourceUser.LikedUsers.Add(userLike); // add user like
            if (await _userRepository.SaveAllAsync()) return Ok(); // if user is saved return ok
            return BadRequest("Failed to like user"); // if user is not saved return bad request
        }
        [HttpGet]
        public async Task<ActionResult<PageList<LikeDto>>> GetUserLikes([FromQuery] LikeParams likeParams)
        {
            likeParams.UserId = User.GetUserId(); // get user id
            var users = await _likesRepository.GetUserLikes(likeParams); // get user likes
            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage,users.PageSize,users.TotalCount,users.TotalPages)); // add pagination header
            return Ok(users); // return users
        }
        
    }
}