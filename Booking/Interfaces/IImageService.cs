using Booking.Models;

namespace Booking.Interfaces
{
    public interface IImageService
    {
        // city
        public Task<bool> AddCityImage(IFormFile file, string folder);
        public void DeleteCityImage(string fileName);
        public Task<bool> UpdateCityImage(string oldImg, IFormFile file, string folder);
        public  Task<string> UploadTourImages(List<IFormFile> files, Tour tour);
    }
}
