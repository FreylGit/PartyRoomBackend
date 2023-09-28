using System;
using PartyRoom.Core.DTOs.Noifications;

namespace PartyRoom.Core.Interfaces.Services
{
	public interface INotificationService
	{
		Task PushInvateRoomAsync(Guid senderId,Guid addresseeId,Guid roomId);

		Task<List<InviteRoomDTO>> GetAllInviteAsync(Guid userId);

		Task InviteReactionAsync(Guid inviteId, bool isConnect);
    }
}

