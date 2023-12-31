﻿using Microsoft.AspNetCore.Mvc;
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

        public AccountController(IAccountService accountService, IUserService userService, JwtService jwtService)
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
            var accessToken = _jwtService.CreateAccessToken(user, claims);
            var refreshToken = _jwtService.GenerateRefreshToken();
            refreshToken.ApplicationUserId = user.Id;
            refreshToken.ApplicationUser = user;
            await _accountService.CreateRefreshTokenAsync(refreshToken);
            SetRefreshToken(refreshToken);
            return Ok(new { token = accessToken });
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(UserRegistrationDTO userRegistration)
        {
            await _userService.CreateUserAsync(userRegistration);
            return await Login(new UserLoginDTO { Email = userRegistration.Email, Password = userRegistration.Password });
        }

        [HttpPost("RegistrationWithUserProfile")]
        public async Task<IActionResult>RegistrationWithUserProfile(RegistrationWithUserProfileDTO model)
        {
            await _userService.CreateUserAsync(model);
            return await Login(new UserLoginDTO { Email = model.UserRegistration.Email, Password = model.UserRegistration.Password });
        }


        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refresh = _jwtService.GetRefreshToken(HttpContext);
            
            var currentRefreshToken = await _accountService.GetRefreshTokenAsync(refresh);
            if (currentRefreshToken == null)
            {
                return BadRequest();
            }
            var user = await _userService.GetUserByIdAsync(currentRefreshToken.ApplicationUserId);

            if (currentRefreshToken.Expires < DateTime.Now)
            {
                var newRefreshToken = _jwtService.GenerateRefreshToken();
                newRefreshToken.ApplicationUserId = currentRefreshToken.ApplicationUserId;
                await _accountService.UpdateRefreshTokenAsync(newRefreshToken);
                SetRefreshToken(newRefreshToken);
            }

            var claims = await _userService.GetClaimsAsync(user);
            var accessToken = _jwtService.CreateAccessToken(user, claims);
            return Ok(new { token = accessToken });
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

        [HttpGet]
        public IActionResult Test()
        {
            return Ok(new { name = "andrey", age = 22 });
        }
    }
}
