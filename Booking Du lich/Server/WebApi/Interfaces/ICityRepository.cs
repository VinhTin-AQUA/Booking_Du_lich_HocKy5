using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface ICityRepository
    {
        public Task<bool> Save();
        public Task<City> GetCityById(int id);
        public Task<bool> AddCity(City city);
        public Task<bool> CityExisted(string name);
        public Task<IEnumerable<City>> GetAllCities();
        public Task<bool> Delete(City city);
        public Task<bool> UpdateCity(City city);
        public Task<IEnumerable<City>> SearchCities(string searchString);
    }
}
