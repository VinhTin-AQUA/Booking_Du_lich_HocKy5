using Booking.Models;

namespace Booking.Interfaces
{
    public interface ITourTypeRepository
    {
        public Task<bool> Save();
        public Task<bool> AddTourType(TourType tourType);
        public Task<TourType?> GetTourTypeByTourId(int tourId);
        public Task<TourType?> GetTourTypeByCategoryId(int categoryId);
        public Task<bool> DeleteTourType(TourType tourType);
        public Task<bool> UpdateTourType(TourType tourType);
    }
}
