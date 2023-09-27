using System;
namespace PartyRoom.Core.DTOs.Room
{
	public class RoomUpdateDTO
	{
        public string? Name { get; set; } = null!;

        public string? Type { get; set; } = null!;

        public decimal? Price { get; set; } = null!;

        public DateTime? StartDate { get; set; } = null!;

        public DateTime? FinishDate { get; set; } = null!;
    }
}

