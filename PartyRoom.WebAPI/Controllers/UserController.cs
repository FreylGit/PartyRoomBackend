using Microsoft.AspNetCore.Mvc;
using PartyRoom.Core.DTOs.User;
using PartyRoom.Core.Interfaces.Services;

namespace PartyRoom.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserRegistrationDTO userRegistration)
        {
            try
            {
                await _userService.CreateUserAsync(userRegistration);
                
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpDelete]
        public async Task <IActionResult >Delete(Guid userId) 
        {
            var user = await _userService.GetUserByIdAsync(userId);
            await _userService.DeleteUserAsync(user);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserDTO user)
        {
            await _userService.UpdateUserAsync(user);
            return Ok();
        }
    }
}
