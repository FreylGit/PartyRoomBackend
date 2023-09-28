using System;
namespace PartyRoom.Core.DTOs.Noifications
{
	public class InviteRoomDTO
	{
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid RoomId { get; set; }

        public string RoomName { get; set; }

    }
}

