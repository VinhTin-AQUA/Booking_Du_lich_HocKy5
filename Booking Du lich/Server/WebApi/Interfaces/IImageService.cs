using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IImageService
    {
        public Task<bool> AddOneToFolder(IFormFile file, string folder);

        public void DeleteCityImage(string fileName);
        public Task<bool> UpdateImage(string oldImg, IFormFile file, string folder);
        public Task<string> UploadImagesHotel(List<IFormFile> files, Hotel hotel);
        public string[] GetAllFileOfFolder(params string[] folder);
        public void DeleteImgHotel(string url);
        public void DeleteAllImgHotel(string hotelId);
        public Task<string> AddRoomImages(List<IFormFile> files, Hotel hotel, Room room);
    }
}
