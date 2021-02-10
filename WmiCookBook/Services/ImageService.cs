using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using WmiCookBook.Services.Interfaces;

namespace WmiCookBook.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _env;

        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }
        
        public async Task<string> UploadFile(string fileBase64, string folderPath = "storage/recipes/")
        {
            fileBase64 = fileBase64.Split("base64,")[1];
            string imageName = Guid.NewGuid() + ".jpg";
            var base64Array = Convert.FromBase64String(fileBase64);
            var filePath = Path.Combine(_env.ContentRootPath, "ClientApp/dist/" + folderPath, imageName);
            await File.WriteAllBytesAsync(filePath, base64Array);
            
            return "/" + folderPath + imageName;
        }
    }
}