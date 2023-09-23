using PartyRoom.Core.DTOs.Tag;

namespace PartyRoom.Core.DTOs.User
{
	public struct RegistrationWithUserProfileDTO
	{
        public UserRegistrationDTO UserRegistration { get; set; }

        public UserProfileDTO UserProfile { get; set; }

        public List<TagCreateDTO> Tags { get; set; }
    }
}

