using System;
using PartyRoom.Core.DTOs.Tag;

namespace PartyRoom.Core.DTOs.User
{
	public class UserProfileUpdateDTO
	{
        public string? About { get; set; } = null;

        public List<TagCreateDTO>? NewTags { get; set; } = null;
    }
}

