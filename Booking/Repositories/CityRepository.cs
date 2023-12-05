using Booking.Data;
using Booking.Interfaces;
using Booking.Models;
using Microsoft.EntityFrameworkCore;


namespace Booking.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly BookingContext context;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IImageService imageService;

        public CityRepository(BookingContext context, IWebHostEnvironment hostEnvironment, IImageService imageService)
        {
            this.context = context;
            this.hostEnvironment = hostEnvironment;
            this.imageService = imageService;
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

        public async Task<City> GetCityById(int? id)
        {
            var city = await context.City.Where(c => c.Id == id).FirstOrDefaultAsync();
            return city;
        }

        public async Task<bool> CityExisted(string name)
        {
            var r = await context.City.Where(c => c.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
            return r != null;
        }

        public async Task<IEnumerable<City>> GetAllCities(string searchString = "")
        {
            List<City> cities;
            if (string.IsNullOrEmpty(searchString))
            {
                cities = await context.City.OrderBy(c => c.Name).ToListAsync();
            }
            else
            {
                cities = await context.City
                    .Where(ct => ct.Name.ToLower().Contains(searchString.ToLower()))
                    .OrderBy(c => c.Name).ToListAsync();
            }
            return cities;
        }

        public async Task<bool> Delete(City city)
        {
            if (city == null)
            {
                return false;
            }
            context.City.Remove(city);
            return await Save();
        }

        public async Task<bool> UpdateCity(City city)
        {
            context.City.Update(city);
            return await Save();
        }

        public async Task<IEnumerable<City>> SearchCities(string searchString)
        {
            var r = await context.City.Where(c => c.Name.ToLower().Contains(searchString.ToLower())).ToListAsync();
            return r;
        }





    }
}
