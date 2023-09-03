using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyRoom.Core.Constants;
using PartyRoom.Core.DTOs.Tag;
using PartyRoom.Core.Entities;
using PartyRoom.Core.Interfaces.Services;
using PartyRoom.Infrastructure.Data;
using PartyRoom.WebAPI.Services;

namespace PartyRoom.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly IProfileService _profileService;

        public ProfileController(JwtService jwtService, IProfileService profileService)
        {
            _jwtService = jwtService;
            _profileService = profileService;
        }

        [HttpGet]
        [Authorize(RoleConstants.RoleUser)]
        public async Task<IActionResult> Get()
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            var user = await _profileService.GetProfileAsync(userId);
            return Ok(user);
        }

        [HttpGet("{userId}")]
        [Authorize(RoleConstants.RoleUser)]
        public async Task<IActionResult> Get(Guid userId)
        {
            var user = await _profileService.GetProfilePublicAsync(userId);
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
        public async Task<IActionResult>AddTag(TagCreateDTO tag)
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            await  _profileService.AddTagAsync(userId,tag);
            return Ok();
        }

        [HttpDelete("DeleteTag")]
        public async Task<IActionResult> DeleteTag(Guid tagId)
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            await _profileService.DeleteTagAsync(userId, tagId);
            return Ok();
        }

    }
}
