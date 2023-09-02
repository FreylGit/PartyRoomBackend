using Microsoft.AspNetCore.Identity;
using PartyRoom.Core.DTOs.User;
using PartyRoom.Core.Entities;
using PartyRoom.Core.Interfaces.Repositories;
using PartyRoom.Core.Interfaces.Services;

namespace PartyRoom.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
       
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountService(IRepository<RefreshToken> refreshTokenRepository,
            IUserRepository userRepository, SignInManager<ApplicationUser> signInManager)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

        public async Task CreateRefreshTokenAsync(RefreshToken refreshToken)
        {
            var refreshFind = await _refreshTokenRepository.GetByIdAsync(refreshToken.ApplicationUserId);
            if(refreshFind != null)
            {
                _refreshTokenRepository.Update(refreshFind);
            }
            else
            {
                await _refreshTokenRepository.AddAsync(refreshToken);
            }
            await _refreshTokenRepository.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(Guid userId)
        {
            var refreshToken = await _refreshTokenRepository.GetByIdAsync(userId);
            return refreshToken;
        }

        public async Task<ApplicationUser> LoginAsync(UserLoginDTO userLogin)
        {
            var user = await _userRepository.GetByEmailAsync(userLogin.Email);
            var result = await _signInManager.PasswordSignInAsync(user, userLogin.Password,false,false);
            if(result.Succeeded)
            {
                return user;
            }
            return null;

        }

        public async Task LogoutAsync(Guid userId)
        {
            var token = await _refreshTokenRepository.GetByIdAsync(userId);
             _refreshTokenRepository.Delete(token);
        }

        public async Task UpdateRefreshTokenAsync(RefreshToken newRefreshToken)
        {
            _refreshTokenRepository.Update(newRefreshToken);
           await _refreshTokenRepository.SaveChangesAsync();
        }
    }
}
