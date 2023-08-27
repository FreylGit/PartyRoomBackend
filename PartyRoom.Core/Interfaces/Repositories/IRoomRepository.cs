using PartyRoom.Core.Entities;

namespace PartyRoom.Core.Interfaces.Repositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<bool> ExistsLinkAsync(string slug);
        Task<Room> GetByLinkAsync(string link);
    }
}
