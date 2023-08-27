using Microsoft.AspNetCore.Identity;
using PartyRoom.Core.Entities;
using PartyRoom.Core.Interfaces.Repositories;
using PartyRoom.Infrastructure.Data;

namespace PartyRoom.Infrastructure.Repositories
{
    public class RoleRepository : RepositoryBase<ApplicationRole>, IRoleRepository
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RoleRepository(ApplicationDbContext context,RoleManager<ApplicationRole> roleManager) : base(context)
        {
            _roleManager = roleManager;
        }
    }
}
