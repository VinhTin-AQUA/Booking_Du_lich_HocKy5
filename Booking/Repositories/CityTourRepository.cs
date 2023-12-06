using Booking.Data;
using Booking.Interfaces;
using Booking.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Repositories
{
    public class CityTourRepository : ICityTourRepository
    {
        private readonly BookingContext context;
        public CityTourRepository(BookingContext context)
        {
            this.context = context;
        }
        public async Task<bool> AddCityTour(CityTour cityTour)
        {
            context.CityTour.Add(cityTour);
            return await Save();
        }

        public async Task<bool> DeleteCityTour(CityTour cityTour)
        {
            if (cityTour == null)
            {
                return false;
            }
            context.CityTour.Remove(cityTour);
            return await Save();
        }

        public async Task<CityTour?> GetCityTourByCityId(int cityId)
        {
            var cityTour = await context.CityTour.Where(ct => ct.CityId == cityId).FirstOrDefaultAsync();
            return cityTour;
        }

        public async Task<CityTour?> GetCityTourByTourId(int tourId)
        {
            var cityTour = await context.CityTour.Where(ct => ct.TourId == tourId).FirstOrDefaultAsync();
            return cityTour;
        }

        public async Task<bool> Save()
        {
            var r = await context.SaveChangesAsync();
            return r > 0;
        }

        public async Task<bool> UpdateCityTour(CityTour cityTour)
        {
            context.CityTour.Update(cityTour);
            return await Save();
        }
    }
}
