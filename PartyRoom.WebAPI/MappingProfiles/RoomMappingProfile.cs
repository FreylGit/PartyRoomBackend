using AutoMapper;
using PartyRoom.Core.DTOs.Noifications;
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

            CreateMap<Room, RoomItemDTO>();

            CreateMap<RoomUpdateDTO, Room>();

            CreateMap<InviteRoomDTO, InviteRoom>();
            CreateMap<InviteRoom, InviteRoomDTO>();
        }
    }
}
