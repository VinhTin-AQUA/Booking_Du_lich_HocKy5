using Azure.Core;
using Booking.Data;
using Booking.Interfaces;
using Booking.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Repositories
{
    public class TourTypeRepository : ITourTypeRepository
    {
        private readonly BookingContext context;
        public TourTypeRepository(BookingContext context)
        {
            this.context = context;
        }
        public async Task<bool> AddTourType(TourCategory tourType)
        {
            context.TourTypes.Add(tourType);
            return await Save();
        }

        public async Task<bool> DeleteTourType(TourCategory tourType)
        {
            if (tourType == null)
                return false;
            context.TourTypes.Remove(tourType);
            return await Save();
        }

        public async Task<TourCategory?> GetTourTypeByCategoryId(int categoryId)
        {
            var tourType = await context.TourTypes.Where(tt => tt.CategoryId == categoryId).SingleOrDefaultAsync();
            return tourType;
        }

        public async Task<TourCategory?> GetTourTypeByTourId(int tourId)
        {
            var tourType = await context.TourTypes.Where(tt => tt.TourId == tourId).SingleOrDefaultAsync();
            return tourType;
        }

        public async Task<bool> Save()
        {
            var s = await context.SaveChangesAsync();
            return s > 0;
        }

        public async Task<bool> UpdateTourType(TourCategory tourType)
        {
            context.TourTypes.Update(tourType);
            return await Save();
        }
    }
}
