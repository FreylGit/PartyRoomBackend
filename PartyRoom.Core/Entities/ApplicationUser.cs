using Microsoft.AspNetCore.Identity;

namespace PartyRoom.Core.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime DateRegistration { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public UserProfile? UserProfile { get; set; }
        public ICollection<Room> CreatedRooms { get; set; }
        public ICollection<UserRoom> UserRoom { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
