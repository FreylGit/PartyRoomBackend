using PartyRoom.Core.Entities;

namespace PartyRoom.Core.Interfaces.Repositories
{
    public interface IUserRoomRepository: IRepository<UserRoom>
    {
        Task<UserRoom> GetByIdAsync(Guid userId, Guid roomId);

        Task<bool> ExistsAsync(Guid userId,Guid roomId);

        // Состоит ли пользователь в комнате 
        Task<bool> ConsistsUserAsync(Guid userId, Guid roomId);
    }
}
