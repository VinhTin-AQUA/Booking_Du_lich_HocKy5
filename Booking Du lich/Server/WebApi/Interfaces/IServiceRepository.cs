using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IServiceRepository
    {
        public Task<bool> Save();
        public Task<HotelService> GetServiceById(int id);
        public Task<bool> AddService(HotelService service);
        public Task<bool> ServiceExisted(string name);
        public Task<IEnumerable<HotelService>> GetAllService();
        public Task<bool> Delete(HotelService hotelService);
    }
}
