using Booking.Models;

namespace Booking.Interfaces
{
    public interface ITourCategoryRepository
    {
        public Task<bool> Save();
        public Task<bool> AddTourCategory(TourCategory tourType);
        public Task<List<TourCategory>?> GetTourCategoryByTourId(int tourId);
        public Task<TourCategory?> GetTourCategoryByCategoryId(int categoryId);
        public Task<bool> DeleteTourCategory(TourCategory tourType);
        public Task<bool> UpdateTourCategory(TourCategory tourType);
        public Task<bool> DeleteTourCategoriesByTourId(int tourId);
    }
}
