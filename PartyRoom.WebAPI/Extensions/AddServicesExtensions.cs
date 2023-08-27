using PartyRoom.Core.Interfaces.Services;
using PartyRoom.Core.Services;
using PartyRoom.WebAPI.Services;

namespace PartyRoom.WebAPI.Extensions
{
    public static class AddServicesExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IImageService,ImageService>();
        }
    }
}
