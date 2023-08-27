namespace PartyRoom.Core.DTOs.User
{
    public class UserPublicDTO
    {
        public string FirtsName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public UserProfileDTO Details { get; set; }
    }
}
