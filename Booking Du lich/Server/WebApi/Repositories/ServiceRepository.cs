using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi1.Data;

namespace WebApi.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext context;

        public ServiceRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddService(HotelService service)
        {
            context.HotelService.Add(service);
            return await Save();
        }

        public async Task<bool> Delete(HotelService hotelService)
        {
            if (hotelService == null)
            {
                return false;
            }
            context.HotelService.Remove(hotelService);
            return await Save();
        }

        public async Task<IEnumerable<HotelService>> GetAllService()
        {
            var services = await context.HotelService.ToListAsync();
            return services;
        }

        public async Task<HotelService> GetServiceById(int id)
        {
            var service = await context.HotelService.Where(s => s.ServiceId == id).FirstOrDefaultAsync();
            return service;
        }

        public async Task<bool> Save()
        {
            var s = await context.SaveChangesAsync();
            return s > 0;
        }

        public async Task<bool> ServiceExisted(string name)
        {
            var s = await context.HotelService.Where(hs => hs.ServiceName.ToLower() == name.ToLower()).FirstOrDefaultAsync();
            return s != null;
        }
    }
}
