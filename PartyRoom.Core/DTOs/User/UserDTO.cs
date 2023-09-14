using PartyRoom.Core.DTOs.Tag;

namespace PartyRoom.Core.DTOs.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string FirtsName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public UserProfileDTO Details { get; set; } = null!;

        public List<TagDTO> Tags { get; set; } = null!;
    }
}
