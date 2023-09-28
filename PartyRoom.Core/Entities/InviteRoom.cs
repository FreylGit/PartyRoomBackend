using System;
namespace PartyRoom.Core.Entities
{
	public class InviteRoom
    {
		public Guid Id { get; set; }

		public Guid AddresseeUserId { get; set; }

		public Guid RoomId { get; set; }

		public Guid SenderUserId { get; set; }

		public ApplicationUser SenderUser { get; set; }

		public Room Room { get; set; }

		public ApplicationUser AddresseeUser { get; set; }
    }
}

