using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IHotelRepository
    {
        public Task<bool> Save();
        public Task<bool> AddHotel(Hotel hotel);
    }
}
