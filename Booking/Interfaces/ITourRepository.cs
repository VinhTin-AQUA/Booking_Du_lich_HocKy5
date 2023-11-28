using Booking.Models;
using Microsoft.AspNetCore.Identity;

namespace Booking.Interfaces
{
    public interface ITourRepository
    {
        public Task<bool> Save();
        public Task<bool> AddTour(Tour tour);
        public Task<Tour> GetTourById(int? id);
        public Task<ICollection<Tour>> GetAllTours();
        public Task<ICollection<Tour>> GetToursOfPoster(string posterId);
        //public Task<IdentityResult> AdddAgent(ApplicationUser agent, string password);
        //public Task<IdentityResult> DeleteAgent(ApplicationUser agent);
        public Task<bool> DeleteTour(Tour tour);
        public Task<bool> UpdateTour(Tour tour);
        public Task<bool> AddTypeToTour(Category tourType, Tour tour);

        public Task<int> TotalAgent(string searchString = "");
        public Task<ICollection<AppUser>> GetAgentTours(int currentPage, int pageSize, string? searchString);
        
    }
}
