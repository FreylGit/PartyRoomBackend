using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PartyRoom.Core.Entities;
using PartyRoom.Core.Interfaces.Repositories;
using PartyRoom.Infrastructure.Data;
using System.Security.Claims;

namespace PartyRoom.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<ApplicationUser>, IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IProfileRepository _profileRepository;
        public UserRepository(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, IProfileRepository profileRepository, RoleManager<ApplicationRole> roleManager) : base(context)
        {
            _userManager = userManager;
            _profileRepository = profileRepository;
            _roleManager = roleManager;
        }

        public async Task AddAsync(ApplicationUser createModel, string password)
        {
            await _userManager.CreateAsync(createModel, password);
        }

        public async Task AddClaimAsync(ApplicationUser user, Claim claim)
        {
            await _userManager.AddClaimAsync(user, claim);
        }

        public async Task AddRoleAsync(ApplicationUser user, ApplicationRole role)
        {
            
            await _userManager.AddToRoleAsync(user, role.Name);
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
           var user =  await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<List<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            return claims.ToList();
        }

        public async Task<ApplicationUser> GetProfileUserByIdAsync(Guid userId)
        {
            var userProfile = await _context.Users.Where(p => p.Id == userId).Include(p => p.UserProfile).FirstOrDefaultAsync();
            return userProfile;
        }
    }
}
