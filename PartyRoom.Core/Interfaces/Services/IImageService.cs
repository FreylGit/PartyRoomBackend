using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyRoom.Core.Interfaces.Services
{
    public interface IImageService
    {
        public Task<Byte[]> GetImageAsync(string url);
        public Task<string> SaveImageAsync(IFormFile file);
        public void DeleteImage(string fileName);
    }
}
