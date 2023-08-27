using Microsoft.EntityFrameworkCore;
using PartyRoom.Core.Entities;
using PartyRoom.Core.Interfaces.Repositories;
using PartyRoom.Infrastructure.Data;

namespace PartyRoom.Infrastructure.Repositories
{
    public class ProfileRepositoty : RepositoryBase<UserProfile>, IProfileRepository
    {
        public ProfileRepositoty(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<UserProfile> GetByIdAsync(Guid userId)
        {
            ApplicationUser userProfile = _context.Users.Where(p => p.Id == userId).Include(p => p.UserProfile).FirstOrDefault();
            return userProfile.UserProfile;
        }
    }
}
