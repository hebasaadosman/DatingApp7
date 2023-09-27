
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public AdminController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IPhotoService photoService, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _photoService = photoService;
            _mapper = mapper;
        }
        [Authorize(Policy = "RequireAdminRole")]

        [HttpGet("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var users = await _userManager.Users
            .OrderBy(user => user.UserName)
            .Include(r => r.UserRoles)
            .Select(u => new
            {
                u.Id,
                Username = u.UserName,
                Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
            }).ToListAsync();

            return Ok(users);
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
        {
            if (roles == null)
            {
                return BadRequest("Roles cannot be null");
            }
            var selectedRoles = roles.Split(",").ToArray();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound("Could not find user");
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
            if (!result.Succeeded)
            {
                return BadRequest("Failed to add to roles");
            }
            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
            if (!result.Succeeded)
            {
                return BadRequest("Failed to remove from roles");
            }
            return Ok(await _userManager.GetRolesAsync(user));
        }




        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("photos-to-moderate")]
        public async Task<ActionResult> GetPhotosForModeration()
        {
            var photos = await _unitOfWork.PhotoRepository.GetUnapprovedPhotos();
            return Ok(photos);
        }
        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpPost("approve-photo/{photoId}")]
        public async Task<ActionResult> ApprovePhoto(int photoId)
        {
            var photo = await _unitOfWork.PhotoRepository.GetPhotoById(photoId);
            if (photo == null) return NotFound("Could not find photo");
            photo.IsApproved = true;
            var user = await _unitOfWork.UserRepository.GetUserByPhotoId(photoId);
            if (!user.Photos.Any(x => x.IsMain))
            {
                photo.IsMain = true;
            }
            if (await _unitOfWork.Complete()) return Ok();
            return BadRequest("Failed to approve photo");
        }
        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpPost("reject-photo/{photoId}")]
        public async Task<ActionResult> RejectPhoto(int photoId)
        {
            var photo = await _unitOfWork.PhotoRepository.GetPhotoById(photoId);
            if (photo == null) return NotFound("Could not find photo");
            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }
            _unitOfWork.PhotoRepository.RemovePhoto(photo);
            if (await _unitOfWork.Complete()) return Ok();
            return BadRequest("Failed to reject photo");

        }
        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());
            var result = await _photoService.AddPhotoAsync(file);
            if (result.Error != null) return BadRequest(result.Error.Message);
            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            user.Photos.Add(photo);
            if (await _unitOfWork.Complete())
            {
                return CreatedAtRoute("GetUser", new
                {
                    username = user.UserName
                }
                , _mapper.Map<PhotoDto>(photo));
            }
            return BadRequest("Problem adding photo");
        }
    }

}