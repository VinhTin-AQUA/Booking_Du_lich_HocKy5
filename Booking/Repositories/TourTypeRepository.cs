using Microsoft.EntityFrameworkCore;
using Booking.Interfaces;
using Booking.Models;
using Booking.Data;

namespace WebApi.Repositories
{
    public class TourTypeRepository : ITourTypeRepository
    {
        private readonly BookingContext context;
        public TourTypeRepository(BookingContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddTourType(TourType type)
        {
            context.TourType.Add(type);
            return await Save();
        }

        public async Task<bool> Delete(TourType type)
        {
            if (type == null)
            {
                return false;
            }
            context.TourType.Remove(type);
            return await Save();
        }

        public async Task<IEnumerable<TourType>> GetAllTourType()
        {
            var types = await context.TourType.ToListAsync();
            return types;
        }

        public async Task<TourType> GetTourTypeById(int? id)
        {
            var type = await context.TourType.Where(t => t.TourTypeId == id).FirstOrDefaultAsync();
            return type;
        }

        public async Task<bool> Save()
        {
            var s = await context.SaveChangesAsync();
            return s > 0;
        }

        public async Task<bool> TourTypeExisted(string name)
        {
            var type = await context.TourType.Where(t => t.TourTypeName.ToLower() == name.ToLower()).FirstOrDefaultAsync();
            return type != null;
        }

        public async Task<bool> UpdateTourType(TourType type)
        {
            context.TourType.Update(type);
            return await Save();
        }
    }
}
