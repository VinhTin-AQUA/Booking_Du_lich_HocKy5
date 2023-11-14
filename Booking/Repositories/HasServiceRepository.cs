using Microsoft.EntityFrameworkCore;
using Booking.Data;
using Booking.Models;
using Booking.Interfaces;

namespace Booking.Repositories
{
    public class HasServiceRepository : IHasServiceRepository
    {
        private readonly BookingContext context;

        public HasServiceRepository(BookingContext context) { this.context = context; }
        public async Task<bool> AddHasService(HasService hasService)
        {
            context.HasServices.Add(hasService);
            return await Save();
        }

        public async Task<bool> DeleteHasService(HasService hasService)
        {
            if (hasService == null)
            {
                return false;
            }
            context.HasServices.Remove(hasService);
            return await Save();
        }

        public async Task<ICollection<HasService>> GetAllHasService()
        {
            var hasServices = await context.HasServices.ToListAsync();
            return hasServices;
        }

        public async Task<HasService?> GetHasServiceByID(int hotelId, int serviceId)
        {
            var hasService = await context.HasServices
                .Where(hs => hs.HotelID == hotelId && hs.ServiceID == serviceId)
                .FirstOrDefaultAsync();
            return hasService;
        }

        public async Task<bool> Save()
        {
            var r = await context.SaveChangesAsync();
            return r > 0;
        }

        public async Task<IEnumerable<HasService>> SearchHotelByService(int serviceId)
        {
            var hasServices = await context.HasServices
                .Where(hs => hs.ServiceID == serviceId).ToListAsync();
                
            return hasServices;
        }

        public async Task<IEnumerable<HasService>> SearchServiceByHotel(int hotelId)
        {
            var hasServices = await context.HasServices
               .Where(hs => hs.HotelID == hotelId)
               .Include(hs => hs.Hotel)
               .Include(hs => hs.Service)
               .ToListAsync();

            return hasServices;
        }

        public async Task<bool> UpdateHasService(HasService hasService)
        {
            context.HasServices.Update(hasService);
            return await Save();
        }
    }
}
