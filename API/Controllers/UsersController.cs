
using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository,
            IMapper mapper)
        {

            _userRepository = userRepository;
            _mapper = mapper;
        }
        // GET: api/Users

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            return Ok(await _userRepository.GetUsersAsync());

        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return Ok(await _userRepository.GetUserByMemberNameAsync(username));
        }
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            // Get the username from the token
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // Get the user from the database
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null) return NotFound("Could not find user");
            // Map the user from the database to the MemberUpdateDto
            _mapper.Map(memberUpdateDto, user);
            // Update the user in the database
            // _userRepository.Update(user);
            // Save the changes to the database
            if (await _userRepository.SaveAllAsync()) return NoContent();
            // If the changes were not saved, return an error
            return BadRequest("Failed to update user");
        }




    }
}