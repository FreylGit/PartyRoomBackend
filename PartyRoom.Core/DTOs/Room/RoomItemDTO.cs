using System;
namespace PartyRoom.Core.DTOs.Room
{
	public class RoomItemDTO
	{
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Type { get; set; } = null!;

        public decimal Price { get; set; }

        public bool IsStarted { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }
    }
}

