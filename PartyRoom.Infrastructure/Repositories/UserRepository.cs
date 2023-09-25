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

        public async Task<bool> ExistsAsync(Guid userId)
        {
            if (await _userManager.FindByIdAsync(userId.ToString()) != null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> ExistsEmailAsync(string email)
        {
            if (await _context.Users.AnyAsync(x => x.Email == email))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ExistsUsernameAsync(string username)
        {
            if(await _context.Users.AnyAsync(x=>x.UserName == username))
            {
                return true;
            }
            return false;
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<List<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            return claims.ToList();
        }
    }
}
