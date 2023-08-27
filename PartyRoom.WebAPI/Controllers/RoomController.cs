using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [Authorize]
        public async Task<IActionResult> Create(RoomCreateDTO roomCreate)
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            await _roomService.CreateAsync(userId, roomCreate);
            return Ok();
        }

        [HttpPost("ConnectToRoom")]
        public async Task<IActionResult> ConnectToRoom(string link)
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            await _roomService.ConnectToRoomAsync(userId, link);
            return Ok();
        }
        [HttpGet("{roomId}")]
        public async Task<IActionResult> Get(Guid roomId)
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            var room = await _roomService.GetRoomAsync(userId, roomId);
            return Ok(room);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = _jwtService.GetUserIdByToken(HttpContext);
            var rooms = await _roomService.GetRoomsAsync(userId);
            return Ok(rooms);
        }
    }
}
