using AutoMapper;
using PartyRoom.Core.DTOs.Room;
using PartyRoom.Core.Entities;

namespace PartyRoom.WebAPI.MappingProfiles
{
    public class RoomMappingProfile:Profile
    {
        public RoomMappingProfile()
        {
            CreateMap<RoomCreateDTO, Room>();
            CreateMap<Room, RoomInfoDTO>();
        }
    }
}
