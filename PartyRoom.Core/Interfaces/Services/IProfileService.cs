using Microsoft.AspNetCore.Http;
using PartyRoom.Core.DTOs.Tag;
using PartyRoom.Core.DTOs.User;
using PartyRoom.Core.Entities;

namespace PartyRoom.Core.Interfaces.Services
{
    public interface IProfileService
    {
        Task AddTagAsync(Guid userId,TagCreateDTO tag);
        Task DeleteTagAsync(Guid userId, Guid tagId);
        Task<UserDTO> GetProfileAsync(Guid id);
        Task<UserPublicDTO> GetProfilePublicAsync(Guid id);
        Task UpdateImageAsync(Guid userId, IFormFile image);
    }
}
