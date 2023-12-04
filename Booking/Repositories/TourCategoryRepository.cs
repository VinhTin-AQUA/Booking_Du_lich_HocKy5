
using Booking.Data;
using Booking.Interfaces;
using Booking.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Repositories
{
    public class TourCategoryRepository : ITourCategoryRepository
    {
        private readonly BookingContext context;
        public TourCategoryRepository(BookingContext context)
        {
            this.context = context;
        }
        public async Task<bool> AddTourCategory(TourCategory tourType)
        {
            context.TourCategories.Add(tourType);
            return await Save();
        }

        public async Task<bool> DeleteTourCategory(TourCategory tourType)
        {
            if (tourType == null)
                return false;
            context.TourCategories.Remove(tourType);
            return await Save();
        }

        public async Task<bool> DeleteTourCategoriesByTourId(int tourId)
        {
            var tourCategories = await context.TourCategories.Where(tt => tt.TourId == tourId).ToListAsync();
            if (tourCategories.Count() == 0)
            {
                return true;
            }
            context.TourCategories.RemoveRange(tourCategories);

            return await Save();
        }

        public async Task<TourCategory?> GetTourCategoryByCategoryId(int categoryId)
        {
            var tourType = await context.TourCategories.Where(tt => tt.CategoryId == categoryId).SingleOrDefaultAsync();
            return tourType;
        }

        public async Task<List<TourCategory>?> GetTourCategoryByTourId(int tourId)
        {
            var tourCategories = await context.TourCategories.Where(tt => tt.TourId == tourId).ToListAsync();
            return tourCategories;
        }

        public async Task<bool> Save()
        {
            var s = await context.SaveChangesAsync();
            return s > 0;
        }

        public async Task<bool> UpdateTourCategory(TourCategory tourType)
        {
            context.TourCategories.Update(tourType);
            return await Save();
        }
    }
}
