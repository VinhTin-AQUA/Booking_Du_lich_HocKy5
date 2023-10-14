using Microsoft.AspNetCore.Identity;
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IHotelRepository
    {
        public Task<bool> Save();
        public Task<bool> AddHotel(Hotel hotel);
        public Task<ICollection<Hotel>> GetAllHotels();
        public Task<Hotel> GetHotelById(int? id);
        public Task<IdentityResult> AdddAgent(ApplicationUser agent, string password);
        public Task<IdentityResult> DeleteAgent(ApplicationUser agent);
        public Task<bool> DeleteHotel(Hotel hotel);
    }
}
