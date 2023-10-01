using System;
namespace PartyRoom.Core.DTOs.Noifications
{
	public class InviteCreateDTO
	{
		public Guid RoomId { get; set; }

		public string UserName { get; set; }
	}
}

