using Booking.Data;
using Booking.Interfaces;
using Booking.Models;
using Booking.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Repositories
{
    public class TourRepository : ITourRepository
    {
        private readonly BookingContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public TourRepository(BookingContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<bool> AddTour(Tour tour)
        {
            context.Tour.Add(tour);
            return await Save();
        }

        public async Task<bool> DeleteTour(Tour tour)
        {
            context.Tour.Remove(tour);
            return await Save();
        }

        public async Task<ICollection<Tour>> GetAllTours()
        {
            var tours = await context.Tour.ToListAsync();
            return tours;
        }

        public async Task<ICollection<Tour>> GetToursOfPoster(string posterId)
        {
            var list = await context.Tour
                .Where(t => t.PosterID == posterId)
                .ToListAsync();
            return list;
        }

        public async Task<Tour> GetTourById(int? id)
        {
            var tour = await context.Tour
                .Where(t => t.TourId == id)
                .SingleOrDefaultAsync();
            return tour;
        }

        public async Task<bool> Save()
        {
            var r = await context.SaveChangesAsync();
            return r > 0;
        }

        public async Task<bool> UpdateTour(Tour tour)
        {
            context.Tour.Update(tour);
            return await Save();
        }

        public async Task<bool> AddTypeToTour(TourCategory tourType, Tour tour)
        {
            tour.TourCategories.Add(tourType);
            context.Tour.Update(tour);
            return await Save();
        }


        public async Task<int> TotalAgent(string searchString = "")
        {
            int totalUser = 0;
            var users = await userManager.GetUsersInRoleAsync("AgentTour");
            if (string.IsNullOrEmpty(searchString))
            {
                totalUser = users.Count();
                return totalUser;
            }

            totalUser = users
                .Where(u => (u.FirstName.ToLower().Contains(searchString.ToLower()) || u.LastName.ToLower().Contains(searchString.ToLower())) && u.Email != SeedAdmin.Email)
                .Count();
            return totalUser;
        }


        public async Task<ICollection<AppUser>> GetAgentTours(int currentPage, int pageSize, string? searchString)
        {
            var users = await userManager.GetUsersInRoleAsync(SeedRole.AGENTTOUR_ROLE);
            if (string.IsNullOrEmpty(searchString))
            {
                var agentTours = users
                .Where(u => u.Email != SeedAdmin.Email)
                .Skip(currentPage * pageSize)
                .Take(pageSize)
                .ToList();
                return agentTours;
            }

            var _agentTours = users
                .Where(u => (u.FirstName.ToLower().Contains(searchString.ToLower()) || u.LastName.ToLower().Contains(searchString.ToLower())) && u.Email != SeedAdmin.Email)
                .Skip(currentPage * pageSize)
                .Take(pageSize)
                .ToList();

            return _agentTours.ToList();
        }
    }
}
