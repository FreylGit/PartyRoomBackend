using System.ComponentModel.DataAnnotations;

namespace PartyRoom.Core.Entities
{
    public class UserProfile
    {
        [Key]
        public Guid ApplicationUserId { get; set; }
        public string? About { get; set; }
        public string? ImagePath { get; set; } 
    }
}
