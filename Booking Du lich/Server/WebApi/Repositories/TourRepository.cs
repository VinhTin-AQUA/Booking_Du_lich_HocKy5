using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Repositories
{
    public class TourRepository : ITourRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public TourRepository(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager,
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
                .Include(t => t.City)
                .ToListAsync();
            return list;
        }

        public async Task<Tour> GetTourById(int? id)
        {
            var tour = await context.Tour
                .Where(t => t.TourId == id)
                .Include(t => t.City)
                .Include(t => t.TourType)
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

        public async Task<bool> AddTypeToTour(TourType tourType, Tour tour)
        {
            tour.TourType = tourType;
            context.Tour.Update(tour);
            return await Save();
        }

        public async Task<ICollection<ApplicationUser>> GetAgentTours()
        {
            var users =await userManager.GetUsersInRoleAsync("AgentTour");
            return users.ToList();
        }
    }
}
