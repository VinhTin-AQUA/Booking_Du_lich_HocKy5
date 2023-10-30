using Microsoft.AspNetCore.Identity;
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface ITourRepository
    {
        public Task<bool> Save();
        public Task<bool> AddTour(Tour tour);
        public Task<Tour> GetTourById(int? id);
        public Task<ICollection<Tour>> GetAllTours();
        //public Task<IdentityResult> AdddAgent(ApplicationUser agent, string password);
        //public Task<IdentityResult> DeleteAgent(ApplicationUser agent);
        public Task<bool> DeleteTour(Tour tour);
        public Task<bool> UpdateTour(Tour tour);
    }
}
