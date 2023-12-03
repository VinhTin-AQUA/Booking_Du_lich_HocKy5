using Booking.Models;

namespace Booking.Interfaces
{
    public interface ITourTypeRepository
    {
        public Task<bool> Save();
        public Task<bool> AddTourType(TourCategory tourType);
        public Task<TourCategory?> GetTourTypeByTourId(int tourId);
        public Task<TourCategory?> GetTourTypeByCategoryId(int categoryId);
        public Task<bool> DeleteTourType(TourCategory tourType);
        public Task<bool> UpdateTourType(TourCategory tourType);
    }
}
