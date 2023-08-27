using PartyRoom.Core.DTOs.Room;

namespace PartyRoom.Core.Interfaces.Services
{
    public interface IRoomService
    {
        Task CreateAsync(Guid authorId,RoomCreateDTO roomCreateDTO);
        Task ConnectToRoomAsync(Guid userId, string link);
        Task<RoomInfoDTO> GetRoomAsync(Guid userId, Guid roomId);
        Task<IEnumerable<RoomInfoDTO>> GetRoomsAsync(Guid userId);
    }
}
