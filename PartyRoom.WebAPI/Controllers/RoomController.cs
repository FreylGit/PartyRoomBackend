using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyRoom.Core.Constants;
using PartyRoom.Core.DTOs.Room;
using PartyRoom.Core.Interfaces.Services;
using PartyRoom.WebAPI.Services;

namespace PartyRoom.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly JwtService _jwtService;
        public RoomController(JwtService jwtService, IRoomService roomService)
        {
            _jwtService = jwtService;
            _roomService = roomService;
        }


        [HttpPost]
        [Authorize(RoleConstants.RoleUser)]
        public async Task<IActionResult> Create(RoomCreateDTO roomCreate)
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            await _roomService.CreateAsync(userId, roomCreate);
            return Ok();
        }

        [HttpPost("ConnectToRoom")]
        [Authorize(RoleConstants.RoleUser)]
        public async Task<IActionResult> ConnectToRoom(string link)
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            await _roomService.ConnectToRoomAsync(userId, link);
            return Ok();
        }

        [HttpGet("{roomId}")]
        [Authorize(RoleConstants.RoleUser)]
        public async Task<IActionResult> Get(Guid roomId)
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            var room = await _roomService.GetRoomAsync(userId, roomId);
            return Ok(room);
        }

        [HttpGet]
        [Authorize(RoleConstants.RoleUser)]
        public async Task<IActionResult> Get()
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            var rooms = await _roomService.GetRoomsAsync(userId);
            return Ok(rooms);
        }

        [HttpGet("ConnectToRoom")]
        [Authorize(RoleConstants.RoleUser)]
        public async Task<IActionResult> ConnectToRoom(Guid roomId)
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            var link = await _roomService.GetConnectLinkToRoomAsync(userId, roomId);

            return Ok(link);
        }

        [HttpDelete]
        [Authorize(RoleConstants.RoleUser)]
        public async Task<IActionResult> Delete(Guid roomId)
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            await _roomService.DeleteAsync(userId, roomId);
            return Ok();
        }

        [HttpDelete("DisconnectUser")]
        [Authorize(RoleConstants.RoleUser)]
        public async Task<IActionResult> DisconnectUser(Guid roomId,Guid? participantId)
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            if (participantId != null)
            {
                await _roomService.DisconnectUserAsync(userId, roomId, participantId!.Value);
                return Ok();
            }

            await _roomService.DisconnectUserAsync(userId, roomId);
            return Ok();
        }
    }
}
