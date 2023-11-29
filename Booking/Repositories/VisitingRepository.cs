using Booking.Data;
using Booking.Interfaces;
using Booking.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Repositories
{
    public class VisitingRepository : IVisitingRepository
    {
        private readonly BookingContext context;
        public VisitingRepository(BookingContext context)
        {
            this.context = context;
        }
        public async Task<bool> AddVisiting(Visiting visiting)
        {
            context.Visitings.Add(visiting);
            return await Save();
        }

        public async Task<bool> DeleteVisiting(Visiting visiting)
        {
            if(visiting == null)
            {
                return false;
            }
            context.Visitings.Remove(visiting);
            return await Save();
        }

        public async Task<Visiting?> GetVisitingByTourId(int tourId)
        {
            var visiting = await context.Visitings.Where(v => v.TourId == tourId).FirstOrDefaultAsync();
            return visiting;
        }

        public async Task<Visiting?> GetVisitingByTouristAttractionId(int touristAttractionId)
        {
             var visiting = await context.Visitings.Where(v => v.TouristAttractionId == touristAttractionId).FirstOrDefaultAsync();
            return visiting;
        }

        public async Task<bool> Save()
        {
            var s = await context.SaveChangesAsync();
            return s > 0;
        }

        public async Task<bool> UpdateVisiting(Visiting visiting)
        {
            context.Visitings.Update(visiting);
            return await Save();
        }
    }
}
