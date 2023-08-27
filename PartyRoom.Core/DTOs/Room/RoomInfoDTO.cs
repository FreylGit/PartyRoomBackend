namespace PartyRoom.Core.DTOs.Room
{
    public class RoomInfoDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public Guid DestinationUserId { get; set; } = Guid.Empty;
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
    }
}
