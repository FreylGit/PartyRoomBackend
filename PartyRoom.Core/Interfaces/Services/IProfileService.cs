using Microsoft.AspNetCore.Http;
using PartyRoom.Core.DTOs.User;

namespace PartyRoom.Core.Interfaces.Services
{
    public interface IProfileService
    {
        Task<UserDTO> GetProfileAsync(Guid id);
        Task<UserPublicDTO> GetProfilePublicAsync(Guid id);
        Task UpdateImageAsync(Guid userId, IFormFile image);
    }
}
