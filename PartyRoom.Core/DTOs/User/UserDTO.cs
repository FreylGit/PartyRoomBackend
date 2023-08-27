﻿namespace PartyRoom.Core.DTOs.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FirtsName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserProfileDTO Details { get; set; }
    }
}
