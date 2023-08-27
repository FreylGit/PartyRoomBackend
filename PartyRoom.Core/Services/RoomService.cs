using AutoMapper;
using PartyRoom.Core.DTOs.Room;
using PartyRoom.Core.Entities;
using PartyRoom.Core.Interfaces.Repositories;
using PartyRoom.Core.Interfaces.Services;

namespace PartyRoom.Core.Services
{
    public class RoomService : IRoomService
    {
        private readonly IMapper _mapper;
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRoomRepository _userRoomRepository;
        public RoomService(IMapper mapper, IRoomRepository roomRepository, IUserRoomRepository userRoomRepository)
        {
            _mapper = mapper;
            _roomRepository = roomRepository;
            _userRoomRepository = userRoomRepository;

        }

        public async Task ConnectToRoomAsync(Guid userId, string link)
        {
            var room = await _roomRepository.GetByLinkAsync(link);
            var userRoom = new UserRoom { Room = room, UserId = userId };
            await _userRoomRepository.AddAsync(userRoom);
            await _userRoomRepository.SaveChangesAsync();
        }

        public async Task CreateAsync(Guid authorId, RoomCreateDTO roomCreate)
        {
            var roomMap = _mapper.Map<Room>(roomCreate);
            roomMap.AuthorId = authorId;
            roomMap.Link = await GenerateUniqueSlug();
            await _roomRepository.AddAsync(roomMap);
            var userRoom = new UserRoom { UserId = authorId, Room = roomMap };
            await _userRoomRepository.AddAsync(userRoom);
            await _roomRepository.SaveChangesAsync();
            await _userRoomRepository.SaveChangesAsync();

        }

        public async Task<RoomInfoDTO> GetRoomAsync(Guid userId, Guid roomId)
        {

            if (!await _userRoomRepository.ExistsAsync(userId, roomId))
            {
                //TODO: Ошибку
                return null;
            }
            var room = await _roomRepository.GetByIdAsync(roomId);
            var roomMap = _mapper.Map<RoomInfoDTO>(room);

            var userRoom = await _userRoomRepository.GetByIdAsync(userId, roomId);
            roomMap.DestinationUserId = userRoom.DestinationUserId;

            return roomMap;
        }

        public async Task<IEnumerable<RoomInfoDTO>> GetRoomsAsync(Guid userId)
        {
            var userRooms = _userRoomRepository.Models.Where(ur => ur.UserId == userId).ToList();
            var rooms = new List<Room>();
            foreach (var userRoom in userRooms)
            {
                var room = await _roomRepository.GetByIdAsync(userRoom.RoomId);
                rooms.Add(room);
            }
            var roomsMap = _mapper.Map<IEnumerable<RoomInfoDTO>>(rooms);
            return roomsMap;
        }

        private async Task<string> GenerateUniqueSlug()
        {
            var length = 12;
            using var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var bytes = new byte[length];
            rng.GetBytes(bytes);

            string slug = Convert.ToBase64String(bytes)
                .Replace("/", "_")
                .Replace("+", "-")
                .Replace("=", "")
                .Substring(0, length);

            while (await _roomRepository.ExistsLinkAsync(slug))
            {
                rng.GetBytes(bytes);
                slug = Convert.ToBase64String(bytes)
                    .Replace("/", "_")
                    .Replace("+", "-")
                    .Replace("=", "")
                    .Substring(0, length);
            }

            return slug;
        }
    }
}
