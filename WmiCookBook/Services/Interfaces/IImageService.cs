using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WmiCookBook.Services.Interfaces
{
    public interface IImageService
    {
        public Task<string> UploadFile(string fileBase64, string folderPath = "storage/recipes/");
    }
}