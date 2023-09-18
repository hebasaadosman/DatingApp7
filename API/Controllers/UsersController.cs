
using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Extensions;
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
        private readonly IPhotoService _photoService;

        public UsersController(IUserRepository userRepository,
            IMapper mapper, IPhotoService photoService)
        {

            _userRepository = userRepository;
            _mapper = mapper;
            _photoService = photoService;
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

            // Get the user from the database
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
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
        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            // Get the username from the token

            // Get the user from the database

            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            if (user == null) return NotFound("Could not find user");
            // Call the AddPhotoAsync method from the PhotoService class and pass in the file
            var result = await _photoService.AddPhotoAsync(file);
            // If the result.Error is not null, return the result.Error.Message
            if (result.Error != null) return BadRequest(result.Error.Message);
            // Set the Url property of the PhotoForCreationDto to the result.SecureUrl.AbsoluteUri
            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            // If the user.Photos.Count is 0, set the IsMain property of the PhotoForCreationDto to true
            if (user.Photos.Count == 0) photo.IsMain = true;
            // Add the photo to the user.Photos collection
            user.Photos.Add(photo);
            // If the changes were saved successfully, return the photo
            if (await _userRepository.SaveAllAsync())
            {
                return CreatedAtAction(nameof(GetUser), new { username = user.UserName }, _mapper.Map<PhotoDto>(photo));
            }
            // If the changes were not saved successfully, return an error
            return BadRequest("Problem adding photo");
        }


        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            if (user == null) return NotFound("Could not find user");
            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
            if (photo == null) return NotFound("Could not find photo");
            if (photo.IsMain) return BadRequest("This is already your main photo");
            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null) currentMain.IsMain = false;
            photo.IsMain = true;
            if (await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to set main photo");

        }
        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId){
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            if (user == null) return NotFound("Could not find user");
            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
            if (photo == null) return NotFound("Could not find photo");
            if (photo.IsMain) return BadRequest("You cannot delete your main photo");
            if (photo.PublicId != null){
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }
            user.Photos.Remove(photo);
            if (await _userRepository.SaveAllAsync()) return Ok();
            return BadRequest("Failed to delete the photo");
        }



    }
}