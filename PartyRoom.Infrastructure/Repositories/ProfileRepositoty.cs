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
            ApplicationUser userProfile = await _context.Users.Where(p => p.Id == userId).Include(p => p.UserProfile).FirstOrDefaultAsync();
            return userProfile.UserProfile;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(Guid userId)
        {
            var userProfile = await _context.Users.Where(p => p.Id == userId).Include(p => p.UserProfile).Include(x=>x.Tags).FirstOrDefaultAsync();
            return userProfile;
        }

        public async Task<ApplicationUser> GetUserByUsernameAsync(string username)
        {
            var userProfile = await _context.Users.Where(p => p.UserName.ToLower() == username.ToLower()).Include(p => p.UserProfile).Include(x => x.Tags).FirstOrDefaultAsync();
            return userProfile;
        }
    }
}
