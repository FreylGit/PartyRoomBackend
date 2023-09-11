using System.ComponentModel.DataAnnotations;

namespace PartyRoom.Core.Entities
{
    public class RefreshToken
    {
        [Key]
        public Guid ApplicationUserId { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DateTime Expires { get; set; } = DateTime.UtcNow.AddDays(7);

        public ApplicationUser ApplicationUser { get; set; }
    }
}
