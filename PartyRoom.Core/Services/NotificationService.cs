using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PartyRoom.Core.DTOs.Noifications;
using PartyRoom.Core.Entities;
using PartyRoom.Core.Interfaces.Repositories;
using PartyRoom.Core.Interfaces.Services;

namespace PartyRoom.Core.Services
{
    public class NotificationService : INotificationService
	{
        private readonly IMapper _mapper;
        private readonly IInviteRoomRepository _inviteRoomRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRoomRepository _userRoomRepository;
        private readonly IRoomService _roomService;
        private readonly IUserRepository _userRepository;

		public NotificationService(IMapper mapper,IInviteRoomRepository inviteRoomRepository,IRoomRepository roomRepository,
            IUserRoomRepository userRoomRepository, IRoomService roomService, IUserRepository userRepository)
		{
            _mapper = mapper;
            _inviteRoomRepository = inviteRoomRepository;
            _roomRepository = roomRepository;
            _userRoomRepository = userRoomRepository;
            _roomService = roomService;
            _userRepository = userRepository;
		}

        public async Task<List<InviteRoomDTO>> GetAllInviteAsync(Guid userId)
        {
            var invitesFind = _inviteRoomRepository.Models.Where(x => x.AddresseeUserId == userId).ToList();

            var invitesMap = _mapper.Map<List<InviteRoomDTO>>(invitesFind);

            foreach (var invite in invitesMap)
            {
                invite.RoomName = await _roomRepository.Models.Where(x => x.Id == invite.RoomId).Select(x => x.Name).FirstOrDefaultAsync();
            }
            return invitesMap;
        }

        public async Task InviteReactionAsync(Guid inviteId, bool isConnect)
        {
            var invite = await _inviteRoomRepository.GetByIdAsync(inviteId);
            if (isConnect)
            {
                await _roomService.ConnectToRoomAsync(invite.AddresseeUserId, invite.RoomId);
            }
            _inviteRoomRepository.Delete(invite);
            await _inviteRoomRepository.SaveChangesAsync();
            
        }

        public async Task PushInvateRoomAsync(Guid senderId, InviteCreateDTO model)
        {

            if(! await _roomRepository.IsAuthorAsync(senderId, model.RoomId))
            {
                throw new InvalidOperationException("Только создатель комнаты может приглашать");
            }

            var addresseeUser = await _userRepository.GetByUserNameAsync(model.UserName);
            if( await _userRoomRepository.ExistsAsync(addresseeUser.Id, model.RoomId))
            {
                throw new InvalidOperationException("Пользователь уже состоит в комнате");
            }
            if(await _inviteRoomRepository.ExistsAsync(addresseeUser.Id, model.RoomId))
            {
                throw new InvalidOperationException("Пользователю уже отправлено приглашение");
            }

            var invite = new InviteRoom
            {
                SenderUserId = senderId,
                AddresseeUserId = addresseeUser.Id,
                RoomId = model.RoomId
            };
            await _inviteRoomRepository.AddAsync(invite);
            await _inviteRoomRepository.SaveChangesAsync();
        }
    }
}

