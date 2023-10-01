using PartyRoom.Core.DTOs.Tag;

namespace PartyRoom.Core.DTOs.User
{
    public class UserPublicDTO
    {
        public Guid Id { get; set; }

        public string FirtsName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public UserProfileDTO Details { get; set; }

        public List<TagDTO> Tags { get; set; }
    }
}
