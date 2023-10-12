using WebApi.Interfaces;
using WebApi.Models;
using WebApi1.Data;

namespace WebApi.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext context;

        public HotelRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Save()
        {
            var r = await context.SaveChangesAsync();
            return r > 0;
        }

        public async Task<bool> AddHotel(Hotel hotel)
        {
            context.Hotel.Add(hotel);
            return await Save();
        }
    }
}
