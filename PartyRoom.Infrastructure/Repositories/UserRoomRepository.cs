using Microsoft.EntityFrameworkCore;
using PartyRoom.Core.Entities;
using PartyRoom.Core.Interfaces.Repositories;
using PartyRoom.Infrastructure.Data;

namespace PartyRoom.Infrastructure.Repositories
{
    public class UserRoomRepository : RepositoryBase<UserRoom>, IUserRoomRepository
    {
        public UserRoomRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsAsync(Guid userId, Guid roomId)
        {
            var userRoom = await _context.UserRoom.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoomId == roomId);
            if (userRoom!=null)
            {
                return true;
            }
            return false;
        }

        public async Task<UserRoom> GetByIdAsync(Guid userId, Guid roomId)
        {
            var userRoom = await _context.UserRoom.FirstOrDefaultAsync(ur => ur.RoomId == roomId && ur.UserId == userId);
            return userRoom;
        }
    }
}
