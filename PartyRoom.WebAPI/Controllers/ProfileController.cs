using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyRoom.Core.Constants;
using PartyRoom.Core.DTOs.Tag;
using PartyRoom.Core.DTOs.User;
using PartyRoom.Core.Entities;
using PartyRoom.Core.Interfaces.Services;
using PartyRoom.WebAPI.Services;

namespace PartyRoom.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly IProfileService _profileService;
        private readonly IUserService _userSerivce;

        public ProfileController(JwtService jwtService, IProfileService profileService, IUserService userService)
        {
            _jwtService = jwtService;
            _profileService = profileService;
            _userSerivce = userService;
        }

        [HttpGet]
        [Authorize(RoleConstants.RoleUser)]
        public async Task<IActionResult> Get()
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            var user = await _profileService.GetProfileAsync(userId);
            return Ok(user);
        }

        [HttpGet("{username}")]
        [Authorize(RoleConstants.RoleUser)]
        public async Task<IActionResult> Get(string username)
        {
            var user = await _profileService.GetProfilePublicAsync(username);
            return Ok(user);
        }

        [HttpPut("UpdateImage")]
        [Authorize(RoleConstants.RoleUser)]
        public async Task<IActionResult> UpdateImage(IFormFile image)
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            await _profileService.UpdateImageAsync(userId, image);
            return Ok();
        }

        [HttpPost("AddTag")]
        [Authorize(RoleConstants.RoleUser)]
        public async Task<IActionResult> AddTag(TagCreateDTO tag)
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            await _profileService.AddTagAsync(userId, tag);
            return Ok();
        }

        [HttpDelete("DeleteTag")]
        [Authorize(RoleConstants.RoleUser)]
        public async Task<IActionResult> DeleteTag(Guid tagId)
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            await _profileService.DeleteTagAsync(userId, tagId);
            return Ok();
        }

        [HttpPut("Update")]
        [Authorize(RoleConstants.RoleUser)]
        public async Task<IActionResult> UpdateAbout(UserProfileUpdateDTO updateProfile)
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            await _profileService.UpdateProfileAsync(updateProfile, userId);
            return Ok();

        }


    }
}
