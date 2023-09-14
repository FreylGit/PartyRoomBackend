namespace PartyRoom.Core.DTOs.Room
{
    public class RoomCreateDTO
    {
        public string Name { get; set; } = null!;

        public string Type { get; set; } = null!;

        public decimal Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }
    }
}
