namespace WebApi.Interfaces
{
    public interface IImageService
    {
        public Task<bool> AddOneToFolder(IFormFile file, string folder);
        public string GetFilePath(string fileName, string folder);
        public void DeleteImage(string fileName);
        public Task<bool> UpdateImage(string oldImg, IFormFile file, string folder);
    }
}
