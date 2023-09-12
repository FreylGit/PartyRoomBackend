using Microsoft.AspNetCore.Http;

namespace PartyRoom.Core.Interfaces.Services
{
    public interface IImageService
    {
        public Task<Byte[]> GetImageAsync(string url);
        public Task<string> SaveImageAsync(IFormFile file);
        public void DeleteImage(string fileName);
    }
}
