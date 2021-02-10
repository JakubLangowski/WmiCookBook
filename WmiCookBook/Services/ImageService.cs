using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        
        public async Task<string> UploadBase64File(string fileBase64, string folderPath = "storage/recipes/")
        {
            fileBase64 = fileBase64.Split("base64,")[1];
            string imageName = Guid.NewGuid() + ".jpg";
            var base64Array = Convert.FromBase64String(fileBase64);
            var filePath = Path.Combine(_env.ContentRootPath, folderPath, imageName);
            await File.WriteAllBytesAsync(filePath, base64Array);
            
            return "/" + folderPath + imageName;
        }

        public async Task<string> UploadFile(IFormFile formFile, string folderPath = "storage/recipes/")
        {
            string imageName = "";  
  
            if (formFile != null)
            {
                imageName = Guid.NewGuid() + Path.GetExtension(formFile.FileName);
                var filePath = Path.Combine(_env.ContentRootPath, folderPath, imageName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))  
                {  
                    await formFile.CopyToAsync(fileStream);  
                }  
            }  
            return "/" + folderPath + imageName;  
        }
    }
}