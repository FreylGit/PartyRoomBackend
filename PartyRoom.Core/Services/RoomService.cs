using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task CheckStartRoomsAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var currentDate = DateTime.UtcNow;

                var roomsToProcess = await _roomRepository.Models
                    .Where(room => room.StartDate <= currentDate && room.IsStarted == false)
                    .ToListAsync(stoppingToken);

                foreach (var room in roomsToProcess)
                {
                    // Устанавливаем что комната запущена
                    room.IsStarted = true;
                    // Формируем пользователям их дарящих
                    await FormationDestinationUser(room.Id);
                    _roomRepository.Update(room);
                }

                // Ожидание некоторое время перед следующей проверкой 
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
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
            if(roomCreate == null)
            {
                throw new ArgumentNullException("Комната пустая");
            }

            if (roomCreate.StartDate >= roomCreate.FinishDate)
            {
                throw new InvalidDataException("Дата начала не может быть позже или такой же как дата конца");
            }

            var roomMap = _mapper.Map<Room>(roomCreate);
            roomMap.AuthorId = authorId;
            roomMap.Link = await GenerateUniqueSlug();
            await _roomRepository.AddAsync(roomMap);
            var userRoom = new UserRoom { UserId = authorId, Room = roomMap };
            await _userRoomRepository.AddAsync(userRoom);
            await _roomRepository.SaveChangesAsync();
            await _userRoomRepository.SaveChangesAsync();
        }

        public async Task<string> GetConnectLinkToRoomAsync(Guid userId, Guid roomId)
        {
            //TODO: Добавить в репозиторий проверку на то что пользователь автор
            var room = await _roomRepository.Models.FirstOrDefaultAsync(x => x.AuthorId == userId && x.Id == roomId);
            if (room == null)
            {
                throw new InvalidOperationException("Пользователь не создатель комнаты");
            }
            return room.Link;
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

        public async Task<IEnumerable<RoomItemDTO>> GetRoomsAsync(Guid userId)
        {
            var rooms = _userRoomRepository.Models.Where(x => x.UserId == userId).Select(x => x.Room).OrderBy(x => x.StartDate).OrderBy(x => x.IsStarted);
            var roomsMap = _mapper.Map<List<RoomItemDTO>>(rooms);
            return roomsMap;
        }

        private async Task<string> GenerateUniqueSlug()
        {
            var length = 16;
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

        private async Task FormationDestinationUser(Guid roomId)
        {
            var userRooms = await _userRoomRepository.Models.Where(ur => ur.RoomId == roomId).ToListAsync();
            var users = userRooms.Select(u => u.UserId);

            List<Guid> availableDestinations = new List<Guid>(userRooms.Count);
            foreach (var userRoom in userRooms)
            {
                availableDestinations.Add(userRoom.UserId);
            }
            Random random = new Random();
            foreach (var userRoom in userRooms)
            {
                int randomIndex = random.Next(availableDestinations.Count);
                // Исключаем возможность выбора текущего UserId в качестве DestinationUserId
                if (userRoom.UserId == availableDestinations[randomIndex])
                {
                    randomIndex = (randomIndex + 1) % availableDestinations.Count;
                }
                availableDestinations.RemoveAt(randomIndex);
            }
            foreach (var item in userRooms)
            {
                _userRoomRepository.Update(item);
            }
            await _userRoomRepository.SaveChangesAsync();
        }
    }
}
