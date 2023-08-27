using Microsoft.AspNetCore.Mvc;
using PartyRoom.Core.Interfaces.Services;

namespace PartyRoom.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }
        [HttpGet("{imagePath}")]
        public async Task<object> GetImage(string imagePath)
        {
            try
            {
                var fileByte = await _imageService.GetImageAsync(imagePath);
                return File(fileByte, "image/jpeg");
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }

        }
    }
}
