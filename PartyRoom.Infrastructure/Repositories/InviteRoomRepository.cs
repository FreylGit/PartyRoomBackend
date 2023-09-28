using System;
using Microsoft.EntityFrameworkCore;
using PartyRoom.Core.Entities;
using PartyRoom.Core.Interfaces.Repositories;
using PartyRoom.Infrastructure.Data;

namespace PartyRoom.Infrastructure.Repositories
{
    public class InviteRoomRepository : RepositoryBase<InviteRoom>, IInviteRoomRepository
    {
		public InviteRoomRepository(ApplicationDbContext context):base(context)
		{
		}

        public async Task<bool> ExistsAsync(Guid userId, Guid roomId)
        {
            if(await _dbSet.AnyAsync(x=>x.AddresseeUserId == userId && x.RoomId == roomId))
            {
                return true;
            }
            return false;
        }
    }
}

