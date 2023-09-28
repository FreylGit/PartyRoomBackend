using System;
using PartyRoom.Core.Entities;

namespace PartyRoom.Core.Interfaces.Repositories
{
	public interface IInviteRoomRepository : IRepository<InviteRoom>
	{
		public Task<bool> ExistsAsync(Guid userId, Guid roomId);

    }
}

