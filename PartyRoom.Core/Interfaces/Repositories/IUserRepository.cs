using PartyRoom.Core.Entities;
using System.Security.Claims;

namespace PartyRoom.Core.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task AddAsync(ApplicationUser user,string password);
        Task AddClaimAsync(ApplicationUser user,Claim  claim);
        Task AddRoleAsync(ApplicationUser user,ApplicationRole role);
        Task<List<Claim>> GetClaimsAsync(ApplicationUser user);
        Task<ApplicationUser> GetProfileUserByIdAsync(Guid userId);
    }
}
