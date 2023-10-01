using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyRoom.Core.Constants;
using PartyRoom.Core.DTOs.Noifications;
using PartyRoom.Core.Interfaces.Services;
using PartyRoom.WebAPI.Services;

namespace PartyRoom.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly JwtService _jwtService;

        public NotificationsController(INotificationService notificationService, JwtService jwtService)
        {
            _notificationService = notificationService;
            _jwtService = jwtService;
        }

        [HttpPost("PushInvite")]
        [Authorize(RoleConstants.RoleUser)]
        public async Task<IActionResult> PushInviteToRoom(InviteCreateDTO model)
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            
            await _notificationService.PushInvateRoomAsync(userId,model);
            return Ok();
        }

        [HttpGet]
        [Authorize(RoleConstants.RoleUser)]
        public async Task<IActionResult> GetInviteToRoom()
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            var invites = await _notificationService.GetAllInviteAsync(userId);
            return Ok(invites);
        }

        // Добавить чтобы передавалось в боди
        [HttpPost("InviteReaction")]
        [Authorize(RoleConstants.RoleUser)]
        public async Task<IActionResult> Reaction(Guid inviteId, bool isConnect = false)
        {
            await _notificationService.InviteReactionAsync(inviteId, isConnect);
            return Ok();
        }
    }
}

