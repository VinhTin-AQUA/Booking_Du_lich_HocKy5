using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IImageService
    {
        // city
        public Task<bool> AddCityImage(IFormFile file, string folder);
        public void DeleteCityImage(string fileName);
        public Task<bool> UpdateCityImage(string oldImg, IFormFile file, string folder);

        // hotel
       
        public void DeleteImgHotel(string url);
        public void DeleteAllImgHotel(string hotelId);

        // room
        public Task<string> AddRoomImages(List<IFormFile> files, Hotel hotel, Room room);
        public string GetFirstImageOfRoom(string photoPath);

        public Task<string> UploadImages(List<IFormFile> files, Hotel hotel, params Room[] room);
        public Task<string> UploadTourImages(List<IFormFile> files, Tour tour);
        public string[] GetAllFileOfFolder(params string[] folder);
    }
}
