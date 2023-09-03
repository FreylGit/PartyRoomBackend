using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using PartyRoom.Core.Entities;
using PartyRoom.Core.Interfaces.Repositories;
using PartyRoom.Infrastructure.Repositories;

namespace PartyRoom.WebAPI.Extensions
{
    public static class AddRepositoriesExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfileRepository, ProfileRepositoty>();
            services.AddScoped<IRepository<RefreshToken>,RefreshTokenRepository>();
            services.AddScoped<IProfileRepository, ProfileRepositoty>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IUserRoomRepository, UserRoomRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
        }
    }
}
