using PartyRoom.Core.DTOs.User;
using PartyRoom.Core.Entities;

namespace PartyRoom.Core.Interfaces.Services
{
    public interface IAccountService
    {
        Task CreateRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetRefreshTokenAsync(Guid userId);
        Task<ApplicationUser> LoginAsync(UserLoginDTO userLogin);
        Task LogoutAsync(Guid userId);
        Task UpdateRefreshTokenAsync(RefreshToken newRefreshToken);
    }
}
