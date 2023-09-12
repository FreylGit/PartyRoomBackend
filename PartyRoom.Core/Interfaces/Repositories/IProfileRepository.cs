using PartyRoom.Core.Entities;

namespace PartyRoom.Core.Interfaces.Repositories
{
    public interface IProfileRepository : IRepository<UserProfile>
    {
        Task<ApplicationUser> GetUserByIdAsync(Guid userId);
    }
}
