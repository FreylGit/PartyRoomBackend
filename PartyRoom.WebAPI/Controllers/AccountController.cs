using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PartyRoom.Core.DTOs.User;
using PartyRoom.Core.Entities;
using PartyRoom.Core.Interfaces.Services;
using PartyRoom.WebAPI.Services;

namespace PartyRoom.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        private IUserService _userService;
        private JwtService _jwtService;
        public AccountController(IAccountService accountService,IUserService userService,JwtService jwtService)
        {
            _accountService = accountService;
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO userLogin)
        {
            var user = await _accountService.LoginAsync(userLogin);
            var claims = await _userService.GetClaimsAsync(user);
            var accessToken = _jwtService.CreateAccessToken(user,claims);
            var refreshToken = _jwtService.GenerateRefreshToken();
            refreshToken.ApplicationUserId = user.Id;
            await _accountService.CreateRefreshTokenAsync(refreshToken);
            SetRefreshToken(refreshToken);
            return Ok(accessToken);
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(UserRegistrationDTO userRegistration)
        {
            try
            {
                await _userService.CreateUserAsync(userRegistration);
                return await Login(new UserLoginDTO { Email = userRegistration.Email,Password = userRegistration.Password});
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            var currentRefreshToken = await _accountService.GetRefreshTokenAsync(userId);
            if (currentRefreshToken == null)
            {
                return BadRequest();
            }
            var user = await _userService.GetUserByIdAsync(currentRefreshToken.ApplicationUserId);

            if (currentRefreshToken.Expires < DateTime.Now)
            {
                var newRefreshToken = _jwtService.GenerateRefreshToken();
                newRefreshToken.ApplicationUserId = currentRefreshToken.ApplicationUserId;
                _accountService.UpdateRefreshTokenAsync(newRefreshToken);
                SetRefreshToken(newRefreshToken);
            }

            var claims = await _userService.GetClaimsAsync(user);
            var accessToken = _jwtService.CreateAccessToken(user, claims);
            return Ok(accessToken);
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOprions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOprions);
        }
    }
}
