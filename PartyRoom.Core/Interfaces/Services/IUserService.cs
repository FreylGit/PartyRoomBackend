using PartyRoom.Core.DTOs.User;
using PartyRoom.Core.Entities;
using System.Security.Claims;

namespace PartyRoom.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(UserRegistrationDTO user);
        Task DeleteUserAsync(ApplicationUser user);
        Task UpdateUserAsync(UserDTO user);
        Task<ApplicationUser> GetUserByIdAsync(Guid userId);
        Task<List<Claim>> GetClaimsAsync(ApplicationUser user);
    }
}
