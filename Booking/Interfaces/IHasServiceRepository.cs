using Booking.Models;

namespace Booking.Interfaces
{
    public interface IHasServiceRepository
    {
        public Task<bool> Save();
        public Task<bool> AddHasService(HasService hasService);
        public Task<ICollection<HasService>> GetAllHasService();
        public Task<HasService?> GetHasServiceByID(int hotelId, int serviceId);

        public Task<bool> DeleteHasService(HasService hasService);


        public Task<IEnumerable<HasService>> SearchHotelByService(int serviceId);
        public Task<IEnumerable<HasService>> SearchServiceByHotel(int hotelId);

        public Task<bool> UpdateHasService(HasService hasService);
    }
}
