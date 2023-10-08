using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface ICityRepository
    {
        public Task<bool> Save();
        public Task<bool> AddCity(City city);
        public Task<bool> CityExisted(string name);
        public Task<IEnumerable<City>> GetAllCities();
    }
}
