using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WmiCookBook.Services.Interfaces
{
    public interface IImageService
    {
        public Task<string> UploadBase64File(string fileBase64, string folderPath = "storage/recipes/");
        public Task<string> UploadFile(IFormFile formFile, string folderPath = "storage/recipes/");
    }
}