using Microsoft.AspNetCore.Http;
using PartyRoom.Core.Interfaces.Services;

namespace PartyRoom.Core.Services
{
    public class ImageService : IImageService
    {
        public void DeleteImage(string fileName)
        {
            var filePath = Path.Combine("StaticFile" + "/" + "Images", fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<Byte[]> GetImageAsync(string imageName)
        {
            if (imageName == null)
            {
                throw new ArgumentException("Failed to recognize image name");
            }
            try
            {
                Byte[] b = await System.IO.File.ReadAllBytesAsync("./StaticFile/Images/" + imageName);
                return b;
            }
            catch
            {
                throw new ArgumentNullException("Could not find an image with that name");
            }
        }

        public async Task<string> SaveImageAsync(IFormFile file)
        {
            // Сгенерировать уникальное имя файла
            var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
            // Создать полный путь к файлу
            var filePath = Path.Combine("./StaticFile" + "/" + "Images", fileName);

            // Сохранить файл в директорию
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}

