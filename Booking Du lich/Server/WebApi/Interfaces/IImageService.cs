using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IImageService
    {
        public Task<bool> AddOneToFolder(IFormFile file, string folder);
        public string GetFilePath(string folder, string fileName);
        public void DeleteImage(string fileName);
        public Task<bool> UpdateImage(string oldImg, IFormFile file, string folder);
        public Task<string> UploadImagesHotel(List<IFormFile> files, Hotel hotel);
    }
}
