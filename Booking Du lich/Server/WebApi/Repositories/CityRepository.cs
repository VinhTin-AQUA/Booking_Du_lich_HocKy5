using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi1.Data;

namespace WebApi.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext context;

        public CityRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Save()
        {
            var r = await context.SaveChangesAsync();
            return r > 0;
        }

        public async Task<bool> AddCity(City city)
        {
            context.City.Add(city);
            return await Save();
        }

        public async Task<bool> CityExisted(string name)
        {
            var r = await context.City.Where(c => c.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
            return r != null;
        }

        public async Task<IEnumerable<City>> GetAllCities()
        {
            var cities = await context.City.OrderBy(c => c.CityCode).ToListAsync();
            return cities;
        }
    }
}
