using Booking.Models;

namespace Booking.Interfaces
{
    public interface ITouristAttraction
    {
        public Task<bool> Save();
        public Task<TouristAttraction> GetTouristAttractionById(int? id);
        public Task<bool> AddTouristAttraction(TouristAttraction touristAttraction);
        public Task<bool> TouristAttractionExisted(string name);
        public Task<IEnumerable<TouristAttraction>> GetAllTouristAttraction(string searchString = "");
        public Task<bool> Delete(TouristAttraction touristAttraction);
        public Task<bool> UpdateCity(TouristAttraction touristAttraction);
        public Task<IEnumerable<TouristAttraction>> SearchTouristAttraction(string searchString);
    }
}
