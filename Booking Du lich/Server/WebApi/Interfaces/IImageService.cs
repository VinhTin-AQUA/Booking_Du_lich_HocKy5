namespace WebApi.Interfaces
{
    public interface IImageService
    {
        public Task<bool> AddOneToFolder(IFormFile file, string folder);
        public string GetFilePath(string fileName, string folder);
    }
}
