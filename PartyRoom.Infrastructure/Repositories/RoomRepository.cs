using Microsoft.EntityFrameworkCore;
using PartyRoom.Core.Entities;
using PartyRoom.Core.Interfaces.Repositories;
using PartyRoom.Infrastructure.Data;

namespace PartyRoom.Infrastructure.Repositories
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsLinkAsync(string slug)
        {
            if (await _context.Rooms.AnyAsync(r => r.Link == slug))
            {
                return true;
            }
            return false;
        }

        public async Task<Room> GetByLinkAsync(string link)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Link == link);
            return room;
        }
    }
}
